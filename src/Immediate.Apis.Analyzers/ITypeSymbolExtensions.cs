using Microsoft.CodeAnalysis;

namespace Immediate.Apis.Analyzers;

internal static class ITypeSymbolExtensions
{
	public static bool IsImmediateApisShared(this INamespaceSymbol symbol) =>
		symbol is
		{
			Name: "Shared",
			ContainingNamespace:
			{
				Name: "Apis",
				ContainingNamespace:
				{
					Name: "Immediate",
					ContainingNamespace.IsGlobalNamespace: true,
				},
			},
		};

	public static bool IsMapMethodAttribute(this ITypeSymbol? typeSymbol, string method) =>
		typeSymbol?.Name == $"Map{method}Attribute"
		&& typeSymbol.ContainingNamespace.IsImmediateApisShared();

	private static readonly string[] s_methods = ["Get", "Post", "Put", "Patch", "Delete"];
	public static bool IsMapMethodAttribute(this ITypeSymbol? typeSymbol) =>
		s_methods.Any(typeSymbol.IsMapMethodAttribute);

	public static bool IsMicrosoftAspNetCoreAuthorization(this INamespaceSymbol symbol) =>
		symbol is
		{
			Name: "Authorization",
			ContainingNamespace:
			{
				Name: "AspNetCore",
				ContainingNamespace:
				{
					Name: "Microsoft",
					ContainingNamespace.IsGlobalNamespace: true,
				},
			},
		};

	public static bool IsAllowAnonymous(this ITypeSymbol? typeSymbol) =>
		typeSymbol?.Name is "AllowAnonymousAttribute"
		&& typeSymbol.ContainingNamespace.IsMicrosoftAspNetCoreAuthorization();

	public static bool IsAuthorize(this ITypeSymbol? typeSymbol) =>
		typeSymbol?.Name is "AuthorizeAttribute"
		&& typeSymbol.ContainingNamespace.IsMicrosoftAspNetCoreAuthorization();

	public static bool IsHandlerAttribute(this ITypeSymbol? typeSymbol) =>
		typeSymbol is
		{
			Name: "HandlerAttribute",
			ContainingNamespace:
			{
				Name: "Shared",
				ContainingNamespace:
				{
					Name: "Handlers",
					ContainingNamespace:
					{
						Name: "Immediate",
						ContainingNamespace.IsGlobalNamespace: true,
					},
				},
			},
		};

	public static bool IsIEndpointConventionBuilder(this ITypeSymbol? typeSymbol) =>
		// IErrorTypeSymbol is until we can reference AspNetCore framework
		typeSymbol is IErrorTypeSymbol or
		{
			Name: "IEndpointConventionBuilder",
			ContainingNamespace:
			{
				Name: "Builder",
				ContainingNamespace:
				{
					Name: "AspNetCore",
					ContainingNamespace:
					{
						Name: "Microsoft",
						ContainingNamespace.IsGlobalNamespace: true,
					},
				},
			},
		};
}
