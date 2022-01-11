﻿using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using ProjetoLinx.webApi.Data;
using Respawn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace ProjetoLinxTestes.Integracao.Setup
{
	public abstract class TestBase : IClassFixture<ApiWebApplicationFactory>, IAsyncLifetime
	{
		public HttpClient Client { get; set; }
		public ProjetoLinxContext Context { get; }
		public IMapper Mapper { get; }
		public IServiceScope Scope { get; }

		public readonly Checkpoint Checkpoint = new()
		{
			TablesToIgnore = new[]{
				"_EFMigrationsHistory"
			},
			DbAdapter = DbAdapter.SqlServer
		};

		// Este código será executado antes de cada teste e antes do InitializeAsync
		public TestBase(ApiWebApplicationFactory factory)
		{
			Client = factory.CreateClient();
			Client.BaseAddress = new Uri("http://localhost:5000/api/");
			Context = factory.Context;
			Mapper = factory.Mapper;
			Scope = factory.Scope;
		}

		// Este código será executado antes de cada teste e depois do construtor TestBase e do construtor da classe filha
		public virtual async Task InitializeAsync()
		{
			await Context.Database.EnsureCreatedAsync();
			Context.ChangeTracker.Clear();
			using var sqlServerConnection= new SqlConnection(Context.StringConnection);
			await sqlServerConnection.OpenAsync();
			await Checkpoint.Reset(sqlServerConnection);
		}

		// O DisposeAsync será executado depois de cada teste
		public async Task DisposeAsync() => await Task.CompletedTask;
	}
}
