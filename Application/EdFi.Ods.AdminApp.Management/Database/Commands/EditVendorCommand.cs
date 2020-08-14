﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using EdFi.Admin.DataAccess.Contexts;
using EdFi.Admin.DataAccess.Models;
using VendorUser = EdFi.Admin.DataAccess.Models.User;

namespace EdFi.Ods.AdminApp.Management.Database.Commands
{
    public class EditVendorCommand
    {
        private readonly IUsersContext _context;

        public EditVendorCommand(IUsersContext context)
        {
            _context = context;
        }

        public void Execute(IEditVendor changedVendorData)
        {
            var vendor = _context.Vendors.Single(v => v.VendorId == changedVendorData.VendorId);
            vendor.VendorName = changedVendorData.Company;

            // because of no UI support for multiple VendorNamespacePrefixes,
            // we will always have only one VendorNamespacePrefix associated with vendor.
            // So, we'll update/remove the first item available.
            if (!string.IsNullOrEmpty(changedVendorData.NamespacePrefix))
            {
                if (vendor.VendorNamespacePrefixes.Any())
                {
                    vendor.VendorNamespacePrefixes.First().NamespacePrefix = changedVendorData.NamespacePrefix;
                }
                else
                {
                    vendor.VendorNamespacePrefixes.Add(new VendorNamespacePrefix
                    {
                        NamespacePrefix = changedVendorData.NamespacePrefix,
                        Vendor = vendor
                    });
                }
            }
            else
            {
                if (vendor.VendorNamespacePrefixes.Any())
                {
                    var toRemove = vendor.VendorNamespacePrefixes.First();
                    _context.VendorNamespacePrefixes.Remove(toRemove);
                }
            }

            if (vendor.Users?.FirstOrDefault() != null)
            {
                vendor.Users.First().FullName = changedVendorData.ContactName;
                vendor.Users.First().Email = changedVendorData.ContactEmailAddress;
            }

            else
            {
                var vendorContact = new VendorUser
                {
                    Vendor = vendor,
                    FullName = changedVendorData.ContactName,
                    Email = changedVendorData.ContactEmailAddress
                };
                vendor.Users = new List<VendorUser> { vendorContact };
            }

            _context.SaveChanges();
        }
    }

    public interface IEditVendor
    {
        int VendorId { get; set; }
        string Company { get; set; }
        string NamespacePrefix { get; set; }
        string ContactName { get; set; }
        string ContactEmailAddress { get; set; }
    }
}
