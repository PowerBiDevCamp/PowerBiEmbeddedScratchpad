using System;
using System.Collections.Generic;
using System.Configuration;

using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;

namespace PowerBiEmbeddedScratchpad.Models {

  class PowerBiApiServiceManager {

    private static Guid workspaceId = new Guid(ConfigurationManager.AppSettings["app-workspace-id"]);
    private static Guid datasetId = new Guid(ConfigurationManager.AppSettings["dataset-id"]);
    private static Guid reportId = new Guid(ConfigurationManager.AppSettings["report-id"]);
    private static Guid dashboardId = new Guid(ConfigurationManager.AppSettings["dashboard-id"]);

    private static Guid datasetWithRlsId = new Guid(ConfigurationManager.AppSettings["rls-dataset-id"]);
    private static Guid reportWithRlsId = new Guid(ConfigurationManager.AppSettings["rls-report-id"]);

    private static string dataset2Id = ConfigurationManager.AppSettings["dataset2-id"];
    private static string dataset3Id = ConfigurationManager.AppSettings["dataset3-id"];

    public static ReportEmbeddingData GetReportEmbeddingDataUserOwnsData() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient(PowerBiPermissionScopes.EditExisitngWorkspaceAssets);

      var report = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);
      var embedUrl = "https://app.powerbi.com/reportEmbed";
      var reportName = report.Name;
      var accessToken = TokenManager.GetAccessToken(PowerBiPermissionScopes.EditExisitngWorkspaceAssets);

      return new ReportEmbeddingData {
        reportId = reportId.ToString(),
        reportName = reportName,
        embedUrl = embedUrl,
        accessToken = accessToken
      };

    }

    public static ReportEmbeddingData GetReportEmbeddingData() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient(PowerBiPermissionScopes.ManageWorkspaceAssets);

      var report = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);
      var embedUrl = "https://app.powerbi.com/reportEmbed";
      var reportName = report.Name;

      // create token request object
      GenerateTokenRequest generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "edit");

      // call to Power BI Service API and pass GenerateTokenRequest object to generate embed token
      string embedToken = pbiClient.Reports.GenerateTokenInGroup(workspaceId,
                                                                 report.Id,
                                                                 generateTokenRequestParameters).Token;

      return new ReportEmbeddingData {
        reportId = reportId.ToString(),
        reportName = reportName,
        embedUrl = embedUrl,
        accessToken = embedToken
      };

    }

    public static DashboardEmbeddingData GetDashboardEmbeddingData() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient();

      var dashboard = pbiClient.Dashboards.GetDashboardInGroup(workspaceId, dashboardId);
      var embedUrl = "https://app.powerbi.com/dashboardEmbed";
      var dashboardDisplayName = dashboard.DisplayName;

      GenerateTokenRequest generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
      string embedToken = pbiClient.Dashboards.GenerateTokenInGroup(workspaceId,
                                                                    dashboardId,
                                                                    generateTokenRequestParameters).Token;

      return new DashboardEmbeddingData {
        dashboardId = dashboardId.ToString(),
        dashboardName = dashboardDisplayName,
        embedUrl = embedUrl,
        accessToken = embedToken
      };

    }

    public static DashboardTileEmbeddingData GetDashboardTileEmbeddingData() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient();

      var tiles = pbiClient.Dashboards.GetTilesInGroup(workspaceId, dashboardId).Value;

      // retrieve first tile in tiles connection
      var tile = tiles[0];
      var tileId = tile.Id;
      var tileTitle = tile.Title;
      var embedUrl = "https://app.powerbi.com/embed";

      GenerateTokenRequest generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
      string embedToken = pbiClient.Tiles.GenerateTokenInGroup(workspaceId,
                                                               dashboardId,
                                                               tileId,
                                                               generateTokenRequestParameters).Token;

      return new DashboardTileEmbeddingData {
        dashboardId = dashboardId.ToString(),
        TileId = tileId.ToString(),
        TileTitle = tileTitle,
        embedUrl = embedUrl,
        accessToken = embedToken
      };

    }

    public static QnaEmbeddingData GetQnaEmbeddingData() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient();

      var dataset = pbiClient.Datasets.GetDatasetInGroup(workspaceId, datasetId.ToString());

      string embedUrl = "https://app.powerbi.com/qnaEmbed";
      string datasetID = dataset.Id;

      GenerateTokenRequest generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");

      string embedToken = pbiClient.Datasets.GenerateTokenInGroup(workspaceId,
                                                                  dataset.Id,
                                                                  generateTokenRequestParameters).Token;

      return new QnaEmbeddingData {
        datasetId = datasetId.ToString(),
        embedUrl = embedUrl,
        accessToken = embedToken
      };

    }

    public static NewReportEmbeddingData GetNewReportEmbeddingData() {


      string embedUrl = "https://embedded.powerbi.com/ReportEmbed";

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient();

      // create embed token request with create permissions
      GenerateTokenRequest generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "create",
                                                                                    datasetId: datasetId.ToString());

      string embedToken = pbiClient.Reports.GenerateTokenForCreateInGroup(workspaceId,
                                                                          generateTokenRequestParameters).Token;

      return new NewReportEmbeddingData {
        workspaceId = workspaceId.ToString(),
        datasetId = datasetId.ToString(),
        embedUrl = embedUrl,
        accessToken = embedToken
      };

    }

    public static NewReportEmbeddingData GetNewReportEmbeddingDataUserOwnsData() {

      string embedUrl = "https://embedded.powerbi.com/ReportEmbed";

      return new NewReportEmbeddingData {
        workspaceId = workspaceId.ToString(),
        datasetId = datasetId.ToString(),
        embedUrl = embedUrl,
        accessToken = TokenManager.GetAccessToken()
      };

    }

    public static ReportWithRlsEmbeddingData GetReportWithRlsEmbeddingData() {

      var userName = "snoopdog@embedme.com";

      PowerBIClient pbiClient = TokenManager.GetPowerBiAppOnlyClient();

      var report = pbiClient.Reports.GetReportInGroup(workspaceId, reportWithRlsId);
      var datasetId = report.DatasetId;
      var embedUrl = "https://app.powerbi.com/reportEmbed";
      var reportName = report.Name;

      GenerateTokenRequest tokenRequestAllData =
        new GenerateTokenRequest(accessLevel: "view",
                                identities: new List<EffectiveIdentity> {
                                  new EffectiveIdentity(username: userName,
                                                        datasets: new List<string> { datasetWithRlsId.ToString() },
                                                        roles: new List<string> { "All Data" })
                                });

      string embedTokenAllData = pbiClient.Reports.GenerateTokenInGroup(workspaceId, reportWithRlsId, tokenRequestAllData).Token;

      EffectiveIdentity identityWesternSales = new EffectiveIdentity(username: userName, roles: new List<string> { "Western Region" }, datasets: new List<string> { datasetWithRlsId.ToString() });
      GenerateTokenRequest tokenRequestWesternSales = new GenerateTokenRequest("view", null, identities: new List<EffectiveIdentity> { identityWesternSales });
      string embedTokenWesternSales = pbiClient.Reports.GenerateTokenInGroup(workspaceId, report.Id, tokenRequestWesternSales).Token;

      EffectiveIdentity identityCentralSales = new EffectiveIdentity(username: userName, roles: new List<string> { "Central Region" }, datasets: new List<string> { datasetWithRlsId.ToString() });
      GenerateTokenRequest tokenRequestCentralSales = new GenerateTokenRequest(accessLevel: "view", datasetId: datasetId, identities: new List<EffectiveIdentity> { identityCentralSales });
      string embedTokenCentralSales = pbiClient.Reports.GenerateTokenInGroup(workspaceId, report.Id, tokenRequestCentralSales).Token;

      EffectiveIdentity identityEasternSales = new EffectiveIdentity(userName, roles: new List<string> { "Eastern Region" }, datasets: new List<string> { datasetWithRlsId.ToString() });
      GenerateTokenRequest tokenRequestEasternSales = new GenerateTokenRequest(accessLevel: "view", datasetId: datasetId, identities: new List<EffectiveIdentity> { identityEasternSales });
      string embedTokenEasternSales = pbiClient.Reports.GenerateTokenInGroup(workspaceId, report.Id, tokenRequestEasternSales).Token;

      EffectiveIdentity identityCombo = new EffectiveIdentity(userName, roles: new List<string> { "Central Region", "Eastern Region" }, datasets: new List<string> { datasetWithRlsId.ToString() });
      GenerateTokenRequest tokenRequestCombo = new GenerateTokenRequest(accessLevel: "view", datasetId: datasetId, identities: new List<EffectiveIdentity> { identityCombo });
      string embedTokenCombo = pbiClient.Reports.GenerateTokenInGroup(workspaceId, report.Id, tokenRequestCombo).Token;


      return new ReportWithRlsEmbeddingData {
        reportId = report.Id.ToString(),
        reportName = reportName,
        embedUrl = embedUrl,
        embedTokenAllData = embedTokenAllData,
        embedTokenWesternSales = embedTokenWesternSales,
        embedTokenCentralSales = embedTokenCentralSales,
        embedTokenEasternSales = embedTokenEasternSales,
        embedTokenCombo = embedTokenCombo
      };

    }

    public static ReportWithAuditingData GetReportWithAuditingData() {

      var userName = "snoopdog@embedme.com";

      PowerBIClient pbiClient = TokenManager.GetPowerBiAppOnlyClient();

      var report = pbiClient.Reports.GetReportInGroup(workspaceId, reportWithRlsId);
      var datasetId = report.DatasetId;
      var embedUrl = "https://app.powerbi.com/reportEmbed";
      var reportName = report.Name;

      GenerateTokenRequest tokenRequest =
        new GenerateTokenRequest(accessLevel: "view",
                                identities: new List<EffectiveIdentity> {
                                  new EffectiveIdentity(username: userName,
                                                        datasets: new List<string> { datasetWithRlsId.ToString() },
                                                        roles: new List<string> { "Eastern Region" })
                                });

      string embedToken = pbiClient.Reports.GenerateTokenInGroup(workspaceId, reportWithRlsId, tokenRequest).Token;

      return new ReportWithAuditingData {
        reportId = report.Id.ToString(),
        reportName = reportName,
        embedUrl = embedUrl,
        embedToken = embedToken,
        userName = userName
      };

    }

    public static ReportWithDynamicBinding GetReportEmbeddingDataDynamicBinding() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient();

      var report = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);
      var embedUrl = "https://app.powerbi.com/reportEmbed";
      var reportName = report.Name;

      // create token request object
      IList<GenerateTokenRequestV2Dataset> datasetRequests =
        new GenerateTokenRequestV2Dataset[] {
          new GenerateTokenRequestV2Dataset(datasetId.ToString()),
          new GenerateTokenRequestV2Dataset(dataset2Id),
          new GenerateTokenRequestV2Dataset(dataset3Id)
        };

      IList<GenerateTokenRequestV2Report> reportRequests =
        new GenerateTokenRequestV2Report[] {
          new GenerateTokenRequestV2Report(reportId)
      };

      IList<GenerateTokenRequestV2TargetWorkspace> workspaceRequests =
        new GenerateTokenRequestV2TargetWorkspace[] {
          new GenerateTokenRequestV2TargetWorkspace(workspaceId)
        };


      GenerateTokenRequestV2 tokenRequest =
        new GenerateTokenRequestV2(datasets: datasetRequests,
                                   reports: reportRequests,
                                   targetWorkspaces: workspaceRequests);


      // call to Power BI Service API and pass GenerateTokenRequest object to generate embed token
      string embedToken = pbiClient.EmbedToken.GenerateToken(tokenRequest).Token;
      return new ReportWithDynamicBinding {
        reportId = reportId.ToString(),
        reportName = reportName,
        embedUrl = embedUrl,
        embedToken = embedToken,
        dataset1Id = datasetId.ToString(),
        dataset2Id = dataset2Id,
        dataset3Id = dataset3Id
      };

    }

    public static ReportEmbeddingData GetReportEmbeddingDataAllDatasets() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient(PowerBiPermissionScopes.TenantReadWriteAll);

      var datasets = pbiClient.Datasets.GetDatasetsInGroup(workspaceId);
      IList<GenerateTokenRequestV2Dataset> datasetRequests = new List<GenerateTokenRequestV2Dataset>();
      foreach (var dataset in datasets.Value) {
        datasetRequests.Add(new GenerateTokenRequestV2Dataset(dataset.Id));
      }


      var report = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);
      var embedUrl = "https://app.powerbi.com/reportEmbed";
      var reportName = report.Name;

      // create token request object
      IList<GenerateTokenRequestV2Report> reportRequests = new GenerateTokenRequestV2Report[] { new GenerateTokenRequestV2Report(report.Id) };
      IList<GenerateTokenRequestV2TargetWorkspace> workspaceRequests = new GenerateTokenRequestV2TargetWorkspace[] { new GenerateTokenRequestV2TargetWorkspace(workspaceId) };


      GenerateTokenRequestV2 tokenRequest =
        new GenerateTokenRequestV2(datasets: datasetRequests,
                                   reports: reportRequests,
                                   targetWorkspaces: workspaceRequests);


      // call to Power BI Service API and pass GenerateTokenRequest object to generate embed token
      string embedToken = pbiClient.EmbedToken.GenerateToken(tokenRequest).Token;
      return new ReportEmbeddingData {
        reportId = reportId.ToString(),
        reportName = reportName,
        embedUrl = embedUrl,
        accessToken = embedToken
      };

    }

    public static ReportEmbeddingData GetPaginatedReportEmbeddingData() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiClient();

      Guid wsId = new Guid("8ecc4996-2746-49c4-a378-6ce0519c57d0");
      Guid pagReportId = new Guid("3ea30594-2470-4be0-8557-023fdb9c65e6");

      var report = pbiClient.Reports.GetReportInGroup(wsId, pagReportId);
      var embedUrl = report.EmbedUrl;
      var reportName = report.Name;


      return new ReportEmbeddingData {
        reportId = pagReportId.ToString(),
        reportName = reportName,
        embedUrl = embedUrl,
        accessToken = TokenManager.GetAccessToken()
      };

    }

    public static ReportEmbeddingData GetPaginatedReportEmbeddingDataAppOnly() {

      PowerBIClient pbiClient = TokenManager.GetPowerBiAppOnlyClient();

      Guid wsId = new Guid("8ecc4996-2746-49c4-a378-6ce0519c57d0");
      Guid pagReportId = new Guid("3ea30594-2470-4be0-8557-023fdb9c65e6");

      var report = pbiClient.Reports.GetReportInGroup(wsId, pagReportId);
      var embedUrl = report.EmbedUrl;
      var reportName = report.Name;

      // create token request object
      GenerateTokenRequest generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "View");

      // call to Power BI Service API and pass GenerateTokenRequest object to generate embed token
      string embedToken = pbiClient.Reports.GenerateTokenInGroup(workspaceId,
                                                                 report.Id,
                                                                 generateTokenRequestParameters).Token;

      return new ReportEmbeddingData {
        reportId = pagReportId.ToString(),
        reportName = reportName,
        embedUrl = embedUrl,
        accessToken = embedToken
      };

    }

  }

}
