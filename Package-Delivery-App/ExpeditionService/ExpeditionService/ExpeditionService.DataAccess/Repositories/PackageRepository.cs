using ExpeditionService.DataAccess.DataContext;
using ExpeditionService.DataAccess.Interfaces;
using ExpeditionService.DataAccess.Models;
using MongoFramework;
using MongoFramework.Linq;
using System.Linq.Expressions;

namespace ExpeditionService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the package table
    /// </summary>
    public class PackageRepository: IPackageRepository
    {
        private readonly ExpeditionContext _expeditionContext;
        private readonly MongoDbSet<Package> _packages;

        /// <summary>
        /// initialization of a new instance of <see cref="PackageRepository"/>
        /// </summary>
        /// <param name="shipmentContext">the database context</param>
        public PackageRepository(ExpeditionContext expeditionContext)
        {
            _expeditionContext = expeditionContext;
            _packages = (MongoDbSet<Package>?)_expeditionContext.Set<Package>(); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to add</param>
        public void AddAsync(Package package) 
        {
            _packages.Add(package);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to delete</param>
        public void DeleteAsync(Package package)
        {
            _packages.Remove(package);
         }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>That contains a List of <seealso cref="Package"/></returns>
        public async Task<List<Package>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _packages
                         .AsNoTracking()
                          .ToListAsync(cancellationToken); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Package> GetBySomethingAsync(Func<Package, bool> predicate, CancellationToken cancellationToken)
        {
            var query = _packages.AsQueryable();
            foreach (var propertyInfo in typeof(Package).GetProperties())
            {
                var parameter = Expression.Parameter(typeof(Package), "x");
                var propertyAccess = Expression.Property(parameter, propertyInfo);
                var value = Expression.Constant(propertyInfo.GetValue(predicate.Target));
                var condition = Expression.Equal(propertyAccess, value);
                var lambda = Expression.Lambda<Func<Package, bool>>(condition, parameter);
                query = query.Where(lambda);
            }
            return await query
                //.AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to update</param>
        public void UpdateAsync(Package package) 
        {
            _packages.Update(package); 
        }
    }
}
