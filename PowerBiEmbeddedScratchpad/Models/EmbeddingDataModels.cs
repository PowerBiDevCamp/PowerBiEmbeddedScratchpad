using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBiEmbeddedScratchpad.Models {

  // data required for embedding a report
  class ReportEmbeddingData {
    public string reportId { get; set; }
    public string reportName { get; set; }
    public string embedUrl { get; set; }
    public string accessToken { get; set; }
  }

  // data required for embedding a dashboard
  class DashboardEmbeddingData {
    public string dashboardId { get; set; }
    public string dashboardName { get; set; }
    public string embedUrl { get; set; }
    public string accessToken { get; set; }
  }

  // data required for embedding a dashboard
  class DashboardTileEmbeddingData {
    public string dashboardId { get; set; }
    public string TileId { get; set; }
    public string TileTitle { get; set; }
    public string embedUrl { get; set; }
    public string accessToken { get; set; }
  }

  // data required for embedding a new report
  class NewReportEmbeddingData {
    public string workspaceId { get; set; }
    public string datasetId { get; set; }
    public string embedUrl { get; set; }
    public string accessToken { get; set; }
  }

  // data required for embedding QnA experience
  class QnaEmbeddingData {
    public string datasetId { get; set; }
    public string embedUrl { get; set; }
    public string accessToken { get; set; }
  }

  // 
  class ReportWithRlsEmbeddingData {
    public string reportId { get; set; }
    public string reportName { get; set; }
    public string embedUrl { get; set; }
    public string embedTokenAllData { get; set; }
    public string embedTokenWesternSales { get; set; }
    public string embedTokenCentralSales { get; set; }
    public string embedTokenEasternSales { get; set; }
    public string embedTokenCombo { get; set; }

  }

  class ReportWithAuditingData {
    public string reportId { get; set; }
    public string reportName { get; set; }
    public string embedUrl { get; set; }
    public string embedToken { get; set; }
    public string userName { get; set; }
  }

  class ReportWithDynamicBinding {
    public string reportId { get; set; }
    public string reportName { get; set; }
    public string embedUrl { get; set; }
    public string embedToken { get; set; }
    public string dataset1Id { get; set; }
    public string dataset2Id { get; set; }
    public string dataset3Id { get; set; }
  }

}
