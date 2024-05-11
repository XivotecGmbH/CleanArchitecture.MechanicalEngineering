using Xivotec.CleanArchitecture.Domain.RecipeAggregate;
using Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Common;

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Repositories;

public class FeatherDeviceRecipeRepository(PostgresPortDataContext dataContext)
    : EfCorePersistentRepository<FeatherDeviceRecipe>(dataContext);