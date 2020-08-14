﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using EdFi.Ods.AdminApp.Management.Database;
using EdFi.Ods.AdminApp.Management.Database.Models;

namespace EdFi.Ods.AdminApp.Management.User
{
    public class GetOdsInstanceRegistrationsByUserIdQuery: IGetOdsInstanceRegistrationsByUserIdQuery
    {
        private readonly AdminAppDbContext _database;

        public GetOdsInstanceRegistrationsByUserIdQuery(AdminAppDbContext database)
        {
            _database = database;
        }

        public IEnumerable<OdsInstanceRegistration> Execute(string userId)
        {
            List<int> instanceRegistrationIds;
            using (var context = AdminAppIdentityDbContext.Create())
            {
                instanceRegistrationIds = context.UserOdsInstanceRegistrations.Where(x => x.UserId == userId).Select(x => x.OdsInstanceRegistrationId).ToList();
            }
            var odsInstanceRegistrations =
                _database.OdsInstanceRegistrations.Where(x => instanceRegistrationIds.Contains(x.Id)).OrderBy(x => x.Name);

            return odsInstanceRegistrations;
        }
    }

    public interface IGetOdsInstanceRegistrationsByUserIdQuery
    {
        IEnumerable<OdsInstanceRegistration> Execute(string userId);
    }
}
