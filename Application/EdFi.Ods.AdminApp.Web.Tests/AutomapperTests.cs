﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using EdFi.Ods.AdminApp.Web.Infrastructure;
using NUnit.Framework;

namespace EdFi.Ods.AdminApp.Web.Tests
{
    [TestFixture]
    public class AutomapperTests
    {
        [Test]
        public void Assert_config_is_valid()
        {
            AutoMapperBootstrapper.CreateMapper().ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
