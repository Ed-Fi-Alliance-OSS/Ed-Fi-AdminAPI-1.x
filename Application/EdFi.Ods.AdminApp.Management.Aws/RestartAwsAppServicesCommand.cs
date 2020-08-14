﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.Threading.Tasks;

namespace EdFi.Ods.AdminApp.Management.Aws
{
    public class RestartAwsAppServicesCommand : IRestartAppServicesCommand
    {
        //This operation isn't currently supported for this platform, but should not raise an exception to the caller.
        public Task Execute(ICloudOdsOperationContext context)
        {
            return Task.CompletedTask;
        }
    }
}
