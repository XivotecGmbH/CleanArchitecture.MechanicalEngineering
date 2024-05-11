using Xivotec.CleanArchitecture.Domain.RecipeAggregate;
using Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Common;

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Repositories;

public class XivotecRecipeRepository(PostgresPortDataContext dataContext)
    : EfCorePersistentRepository<XivotecRecipe>(dataContext);