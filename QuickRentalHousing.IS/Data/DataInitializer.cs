// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickRentalHousing.IS.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResource = Duende.IdentityServer.EntityFramework.Entities.ApiResource;
using ApiScope = Duende.IdentityServer.EntityFramework.Entities.ApiScope;
using Client = Duende.IdentityServer.EntityFramework.Entities.Client;
using IdentityResource = Duende.IdentityServer.EntityFramework.Entities.IdentityResource;

namespace QuickRentalHousing.IS.Data
{
    public class DataInitializer
    {
        private static readonly IEnumerable<string> ROLE_NAMES = new string[]
        {
            USER_ROLES.ADMIN,
            USER_ROLES.MANAGER,
            USER_ROLES.STAFF,
            USER_ROLES.HOME_OWNER,
            USER_ROLES.TENANT,
        };

        private static readonly IEnumerable<(string Username, string Password, IEnumerable<string> Roles)>
            USER_INFOS = new (string Username, string Password, IEnumerable<string> Roles)[]
            {
                ("admin", "Pass123$", new []
                {
                    USER_ROLES.ADMIN,
                }),
            };

        // Identity Resource Seeding
        private const string QUICK_RENTAL_HOUSING_API = "QuickRentalHousing.Api";
        private static readonly IEnumerable<IdentityResource> IDENTITY_RESOURCES = new IdentityResource[]
            {
                new IdentityResources.OpenId().ToEntity(),
                new IdentityResources.Profile().ToEntity(),
                new IdentityResource
                {
                    Name = QUICK_RENTAL_HOUSING_API,
                }
            };

        // API Scope Seeding
        private const string API_SCOPE_READ = "read";
        private const string API_SCOPE_WRITE = "write";
        private const string API_SCOPE_DELETE = "delete";
        private static readonly IEnumerable<string> API_SCOPE_NAMES = new string[] {
            API_SCOPE_READ,
            API_SCOPE_WRITE,
            API_SCOPE_DELETE,
        };

        // API Resource Seeding
        private static readonly IEnumerable<string> API_RESOURCE_NAMES = new string[]
        {
        };

        // Client Seeding
        private static readonly IEnumerable<Client> CLIENTS = new Client[]
        {
            new Client
            {
                ClientId = "Postman",
                ClientSecrets = new string[] { "TestPassword".Sha256() }.Select(x => new ClientSecret
                {
                    Value = x,
                }).ToList(),
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword.Select(x => new ClientGrantType
                {
                    GrantType = x
                }).ToList(),
                AllowOfflineAccess = true,
                AllowedScopes = new string[]
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    QUICK_RENTAL_HOUSING_API
                }.Select(x => new ClientScope
                {
                    Scope = x,
                }).ToList(),
            },
            new Client
            {
                ClientId = QUICK_RENTAL_HOUSING_API,
                ClientSecrets = new string[] { "TestPassword".Sha256() }.Select(x => new ClientSecret
                {
                    Value = x,
                }).ToList(),
                AllowedGrantTypes = GrantTypes.Code.Select(x => new ClientGrantType
                {
                    GrantType = x
                }).ToList(),
                AllowedScopes = new string[]
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    QUICK_RENTAL_HOUSING_API
                }.Select(x => new ClientScope
                {
                    Scope = x,
                }).ToList(),
            },
            new Client
            {
                ClientId = "QuickRentalHousing.FE.Dev",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code.Select(x => new ClientGrantType
                {
                    GrantType = x
                }).ToList(),
                RequirePkce = true,
                AllowedScopes = new string[]
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    QUICK_RENTAL_HOUSING_API
                }.Select(x => new ClientScope
                {
                    Scope = x,
                }).ToList(),

                RedirectUris = new string[]
                {
                    "https://localhost:5002/authentication/login-callback"
                }.Select(x => new ClientRedirectUri
                {
                    RedirectUri = x,
                }).ToList(),
                PostLogoutRedirectUris = new string[]
                {
                    "https://localhost:5002/authentication/logout-callback"
                }.Select(x => new ClientPostLogoutRedirectUri
                {
                    PostLogoutRedirectUri = x,
                }).ToList(),
                AllowedCorsOrigins = new string[]
                {
                    "https://localhost:5002",
                }.Select(x => new ClientCorsOrigin
                {
                    Origin = x
                }).ToList(),
            },
            new Client
            {
                ClientId = "QuickRentalHousing.FE.Production",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code.Select(x => new ClientGrantType
                {
                    GrantType = x
                }).ToList(),
                RequirePkce = true,
                AllowedScopes = new string[]
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    QUICK_RENTAL_HOUSING_API
                }.Select(x => new ClientScope
                {
                    Scope = x,
                }).ToList(),

                RedirectUris = new string[]
                {
                    "https://localhost:5002/authentication/login-callback"
                }.Select(x => new ClientRedirectUri
                {
                    RedirectUri = x,
                }).ToList(),
                PostLogoutRedirectUris = new string[]
                {
                    "https://localhost:5002/authentication/logout-callback"
                }.Select(x => new ClientPostLogoutRedirectUri
                {
                    PostLogoutRedirectUri = x,
                }).ToList(),
                AllowedCorsOrigins = new string[]
                {
                    "https://localhost:5002",
                }.Select(x => new ClientCorsOrigin
                {
                    Origin = x
                }).ToList(),
            }
        };

        public static async Task ExecuteAsync(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var scopeServiceProvider = serviceScope.ServiceProvider;

            await SeedData_RolesAsync(scopeServiceProvider);
            await SeedData_ApplicationUsersAsync(scopeServiceProvider);

            {
                var configurationDbContext = scopeServiceProvider.GetService<ConfigurationDbContext>();
                var webHostEnvironment = scopeServiceProvider.GetService<IWebHostEnvironment>();
                if (webHostEnvironment.IsDevelopment())
                {
                    await configurationDbContext.Database.EnsureDeletedAsync();
                    await configurationDbContext.Database.MigrateAsync();
                }

                await SeedData_IdentityResourcesAsync(configurationDbContext);
                await SeedData_ApiScopesAsync(configurationDbContext);
                await SeedData_ApiResourcesAsync(configurationDbContext);
                await SeedData_ClientsAsync(configurationDbContext);

                await configurationDbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedData_RolesAsync(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var scopeServiceProvider = serviceScope.ServiceProvider;

            var roleManager = scopeServiceProvider.GetService<RoleManager<IdentityRole>>();
            foreach (var item in ROLE_NAMES)
            {
                if (await roleManager.Roles.AnyAsync(x => x.Name == item) == false)
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item,
                    });
                }
            }
        }

        private static async Task SeedData_ApplicationUsersAsync(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var scopeServiceProvider = serviceScope.ServiceProvider;

            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            foreach (var userInfo in USER_INFOS)
            {
                if (await userManager.Users.AnyAsync(x => x.UserName == userInfo.Username) == false)
                {
                    var applicationUser = new ApplicationUser
                    {
                        UserName = userInfo.Username,
                        NormalizedUserName = userInfo.Username.ToUpper(),
                    };
                    applicationUser.PasswordHash = passwordHasher.HashPassword(applicationUser,
                        userInfo.Password);

                    await userManager.CreateAsync(applicationUser);

                    foreach (var role in userInfo.Roles)
                    {
                        await userManager.AddToRoleAsync(applicationUser, role);
                    }
                }
            }
        }

        private static async Task SeedData_IdentityResourcesAsync(ConfigurationDbContext configurationDbContext)
        {
            var seedingTable = configurationDbContext.IdentityResources;
            foreach (var item in IDENTITY_RESOURCES)
            {
                if (await seedingTable.AnyAsync(x => x.Name == item.Name) == false)
                {
                    await seedingTable.AddAsync(item);
                }
            }
        }

        private static async Task SeedData_ApiScopesAsync(ConfigurationDbContext configurationDbContext)
        {
            var seedingTable = configurationDbContext.ApiScopes;
            foreach (var item in API_SCOPE_NAMES)
            {
                if (await seedingTable.AnyAsync(x => x.Name == item) == false)
                {
                    await seedingTable.AddAsync(new ApiScope
                    {
                        Name = item,
                    });
                }
            }
        }

        private static async Task SeedData_ApiResourcesAsync(ConfigurationDbContext configurationDbContext)
        {
            var seedingTable = configurationDbContext.ApiResources;
            foreach (var item in API_RESOURCE_NAMES)
            {
                if (await seedingTable.AnyAsync(x => x.Name == item) == false)
                {
                    await seedingTable.AddAsync(new ApiResource
                    {
                        Name = item,
                    });
                }
            }
        }

        private static async Task SeedData_ClientsAsync(ConfigurationDbContext configurationDbContext)
        {
            var seedingTable = configurationDbContext.Clients;
            foreach (var item in CLIENTS)
            {
                if (await seedingTable.AnyAsync(x => x.ClientId == item.ClientId) == false)
                {
                    await seedingTable.AddAsync(item);
                }
            }
        }
    }
}
