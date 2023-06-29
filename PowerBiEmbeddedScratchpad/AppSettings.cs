using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBiEmbeddedScratchpad {
  public class AppSettings {

    public const string WorkspaceId = "11111111-1111-1111-1111-111111111111";
    public const string DatasetId = "22222222-2222-2222-2222-222222222222";
    public const string ReportId = "33333333-3333-3333-3333-333333333333";
    public const string DashboardId = "44444444-4444-4444-4444-444444444444";

    public const string DatasetWithRlsId = "55555555-5555-5555-5555-555555555555";
    public const string ReportWithRlsId = "66666666-6666-6666-6666-666666666666";
    public const string Dataset2Id = "77777777-7777-7777-7777-777777777777";
    public const string Dataset3Id = "88888888-8888-8888-8888-888888888888";

    // public Azure AD application metadata for user authentication
    public const string ApplicationId = "99999999-9999-9999-9999-999999999999";
    public const string RedirectUri = "http://localhost";

    // confidential Azure AD application metadata for service principal authentication
    public const string TenantId = "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA";
    public const string ConfidentialApplicationId = "BBBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB";
    public const string ConfidentialApplicationSecret = "APP_SECRET_HERE";
    public const string TenantSpecificAuthority = "https://login.microsoftonline.com/" + TenantId;

    // Admin user added to workspaces created by service principal
    public const string EffectiveUserIdentity = "user1@company1.onMicrosoft.com";

    // local file path to export JSON files which let you inpsect datasource definitions
    public const string LocalPagesFolder = @"C:\DevCampLabs\PowerBiEmbeddedScratchpad\LivePages\";



  }
}
