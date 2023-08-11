﻿using IdentityService.DataAccess.DataContext;
using IdentityService.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the save changes repository
    /// </summary>
    public class SaveChangesRepository : ISaveChangesRepository
    {
        private readonly IdentityContext _indentityContext;
        private readonly ILogger<SaveChangesRepository> _logger;

        /// <summary>
        ///  Initializes a new instance of <see cref="SaveChangesRepository"/>
        /// </summary>
        /// <param name="identityContext">The identity context</param>
        /// <param name="logger">The logger</param>
        public SaveChangesRepository(IdentityContext identityContext, ILogger<SaveChangesRepository> logger)
        {
            _indentityContext = identityContext;
            _logger = logger;
        }

        /// <summary>
        /// Saving the changes to the database
        /// </summary>
        /// <param name="cancellationToken">The cancellation cancellationToken</param>
        /// <returns></returns>
        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Saving the changes to the database");

            return _indentityContext.SaveChangesAsync(cancellationToken);
        }
    }
}
