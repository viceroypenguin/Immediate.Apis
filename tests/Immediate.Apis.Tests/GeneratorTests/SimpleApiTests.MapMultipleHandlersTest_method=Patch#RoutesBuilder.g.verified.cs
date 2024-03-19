﻿//HintName: RoutesBuilder.g.cs
using Microsoft.AspNetCore.Mvc;

#pragma warning disable CS1591

namespace Microsoft.AspNetCore.Builder;

public static class TestsRoutesBuilder
{
	public static IEndpointRouteBuilder MapTestsEndpoints(
		this IEndpointRouteBuilder app
	)
	{
		_ = app
			.MapPatch(
				"/test",
				async (
					[AsParameters] global::Dummy.GetUsersQuery.Query parameters,
					[FromServices] global::Dummy.GetUsersQuery.Handler handler,
					CancellationToken token
				) => await handler.HandleAsync(parameters, token)
			)
			;

		_ = app
			.MapPatch(
				"/test",
				async (
					[AsParameters] global::Dummy.GetUserQuery.Query parameters,
					[FromServices] global::Dummy.GetUserQuery.Handler handler,
					CancellationToken token
				) => await handler.HandleAsync(parameters, token)
			)
			;

		return app;
	}
}
