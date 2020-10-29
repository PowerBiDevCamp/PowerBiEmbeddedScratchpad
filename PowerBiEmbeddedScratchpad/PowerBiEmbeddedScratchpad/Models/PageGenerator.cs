using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace PowerBiEmbeddedScratchpad.Models {
  class PageGenerator {

    #region "Internal implementation details"

    private static readonly string rootFolder = ConfigurationManager.AppSettings["local-pages-folder"];

    static PageGenerator() {
      Directory.CreateDirectory(rootFolder);
      Directory.CreateDirectory(rootFolder + "css");
      File.WriteAllText(rootFolder + "css/app.css", Properties.Resources.app_css);
      File.WriteAllText(rootFolder + "css/bootstrap.css", Properties.Resources.bootstrap_css);
      Directory.CreateDirectory(rootFolder + "css/img");
      File.WriteAllBytes(rootFolder + "favicon.ico", Properties.Resources.favicon_ico);
      File.WriteAllBytes(rootFolder + "css/img/loading.gif", Properties.Resources.loading_gif);
      File.WriteAllBytes(rootFolder + "css/img/loading2.gif", Properties.Resources.loading2_gif);
      File.WriteAllBytes(rootFolder + "css/img/loading3.gif", Properties.Resources.loading3_gif);
      File.WriteAllBytes(rootFolder + "css/img/background.jpg", Properties.Resources.background_jpg);
      File.WriteAllBytes(rootFolder + "css/img/menuicon.png", Properties.Resources.menuicon_png);
      Directory.CreateDirectory(rootFolder + "scripts");
      File.WriteAllText(rootFolder + "scripts/jquery.js", Properties.Resources.jquery_js);
      File.WriteAllText(rootFolder + "scripts/bootstrap.js", Properties.Resources.bootstrap_js);
      File.WriteAllText(rootFolder + "scripts/powerbi.min.js", Properties.Resources.powerbi_min_js);
      File.WriteAllText(rootFolder + "scripts/powerbi.js", Properties.Resources.powerbi_js);
      File.WriteAllText(rootFolder + "scripts/powerbi.js.map", Properties.Resources.powerbi_js_map);
    }

    static private void LaunchPageInBrowser(string pagePath) {
      Process.Start("chrome.exe", " --new-window --app=" + pagePath);
    }

    #endregion

    // demo 01
    public static void GenerateReportPageUserOwnsData(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingDataUserOwnsData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReport_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo01: Embed Report - User-Owns-Data")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken)
                                    .Replace("EmbedTokenType", "models.TokenType.Aad");


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo01-EmbedReport-UserOwnsData.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 02
    public static void GenerateReportPageAppOwnsData(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReport_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo02: Embed Report - App-Owns-Data")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken)
                                    .Replace("EmbedTokenType", "models.TokenType.Embed");



      // generate page file on local har drive
      string pagePath = rootFolder +  "Demo02-EmbedReport-AppOwnsData.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 03
    public static void GenerateDashboardPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetDashboardEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedDashboard_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo03: Embed Dashboard")
                                    .Replace("@EmbedDashboardId", embeddingData.dashboardId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo03-EmbedDashboard.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 04
    public static void GenerateDashboardTilePage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetDashboardTileEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedDashboardTile_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo04: Dashboard Tile")
                                    .Replace("@EmbedDashboardId", embeddingData.dashboardId)
                                    .Replace("@EmbedTileId", embeddingData.TileId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo04-EmbedDashboardTile.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 05
    public static void GenerateQnaPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetQnaEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedQna_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo05: QnA Experience")
                                    .Replace("@DatasetId", embeddingData.datasetId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo05-EmbedQnA.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 06
    public static void GenerateReportPageEditMode(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportEditMode_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo06: Edit Report")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken)
                                    .Replace("EmbedTokenType", "models.TokenType.Embed");


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo06-EmbedReport-EditMode.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 07
    public static void GenerateReportWithToolbarPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingDataAllDatasets();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithToolbar_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo07: Report Toolbar")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo07-EmbedReport-CustomToolbar.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 08
    public static void GenerateNewReportPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetNewReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedNewReport_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo08: New Report")
                                    .Replace("@EmbedWorkspaceId", embeddingData.workspaceId)
                                    .Replace("@EmbedDatasetId", embeddingData.datasetId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo08-EmbedNewReport-NoSaveAsSupport.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 09
    public static void GenerateNewReportPageWithSaveAsRedirect(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetNewReportEmbeddingDataUserOwnsData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedNewReportWithSaveAsRedirect_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo08: New Report with SaveAs")
                                    .Replace("@EmbedWorkspaceId", embeddingData.workspaceId)
                                    .Replace("@EmbedDatasetId", embeddingData.datasetId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo09-EmbedNewReport-WithSaveAsRedirect.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

   // demo 10
    public static void GenerateReportPageWithTransparentBackground(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithTransparentBackground_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo10: Transparent Background")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo10-EmbedReport-TransparentBackground.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 11
    public static void GenerateReportPageWithThemeLoading(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithThemeLoading_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo11: Apply Themes")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo11-EmbedReport-ApplyTheme.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 12
    public static void GenerateReportPageWithContrastMode(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithContrastMode_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo12: Contrast Mode")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo12-EmbedReport-ContrastMode.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 13
    public static void GenerateReportPageWithLocalizedLanguage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithLocalizedLanguage_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo13: Localized Language")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo13-EmbedReport-LocalizedLanguage.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 14
    public static void GenerateReportWithEventLogging(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithEventLogging_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo14: Events Demo")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo14-EmbedReport-EventLogging.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 15
    public static void GenerateReportWithPageNavigation(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithPageNavigation_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo15: Page Navigation")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo15-EmbedReport-CustomPageNavigation.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 16
    public static void GenerateReportWithBookmarks(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithBookmarks_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo16: Bookmarks")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo16-EmbedReport-CustomBookmarksNavigation.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 17
    public static void GenerateReportWithBookmarkPlayMode(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithBookmarksPlayMode_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo17: Bookmarks Play Mode")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo17-EmbedReport-BookmarksPlayMode.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 18
    public static void GenerateReportWithBookmarkCarousel(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithBookmarkCarousel_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo18: Bookmark Carousel")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo18-EmbedReport-BookmarkCarousel.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 19
    public static void GenerateReportWithCustomFiltering(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithCustomFiltering_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo19: Custom Filtering")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo19-EmbedReport-CustomFiltering.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 20
    public static void GenerateReportPageWithPhasedLoading(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithPhasedLoading_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo20: Phased Loading")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo20-EmbedReport-PhasedLoading.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 21
    public static void GenerateReportPageWithWhiteLabelLoading(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithWhiteLabelLoading_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo21: White Label Loading")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo21-EmbedReport-WhiteLabelLoading.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 22
    public static void GenerateReportPageWithBootstrapOptimization(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithBootstrap_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo22: Bootstrap Optimization")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo22-EmbedReport-BootstrapOptimization.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 23
    public static void GenerateReportWithContextMenusPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithContextMenus_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo23-Custom Menus")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo23-EmbedReport-CustomContextMenus.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 24
    public static void GenerateReportWithCustomExportCommand(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithCustomExportCommand_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo24 - Custom Export Command")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo24-EmbedReport-CustomExportCommand.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 25
    public static void GenerateReportInspectorPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.ReportInspector_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo25: Visual Inspector")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo25-EmbedReport-VisualInspector.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 26
    public static void GenerateReportVisualPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportVisual_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo26: Single Visual")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo26-EmbedReport-SingleVisual.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 27
    public static void GenerateReportWithCustomLayout(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithCustomLayout_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo27: Custom Layouts")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);

      // generate page file on local har drive
      string pagePath = rootFolder + "Demo27-EmbedReport-CustomLayout.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 28
    public static void GenerateReportWithRls(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportWithRlsEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithRLS_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo28: Row Level Security")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedTokenAllData", embeddingData.embedTokenAllData)
                                    .Replace("@EmbedTokenWesternSales", embeddingData.embedTokenWesternSales)
                                    .Replace("@EmbedTokenCentralSales", embeddingData.embedTokenCentralSales)
                                    .Replace("@EmbedTokenEasternSales", embeddingData.embedTokenEasternSales)
                                    .Replace("@EmbedTokenCombo", embeddingData.embedTokenCombo);

      // generate page file on local har drive
      string pagePath = rootFolder + "Demo28-EmbedReport-RowLevelSecurity.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 29
    public static void GenerateReportWithCorrelationId(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportWithAuditingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithCorrelationId_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo29: Correlation ID")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.embedToken)
                                    .Replace("@UserName", embeddingData.userName);

      // generate page file on local har drive
      string pagePath = rootFolder + "Demo29-EmbedReport-CorrelationId.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 30
    public static void GenerateReportWithDynamicBinding(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingDataDynamicBinding();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithDynamicBinding_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo30: Dynamic Binding")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.embedToken)
                                    .Replace("@DatasetId", embeddingData.dataset1Id)
                                    .Replace("@Dataset2Id", embeddingData.dataset2Id)
                                    .Replace("@Dataset3Id", embeddingData.dataset3Id);

      // generate page file on local har drive
      string pagePath = rootFolder + "Demo30-EmbedReport-DynamicBinding.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 31
    public static void GenerateNewReportCreateVisual(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedNewReportCreateVisuals_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo31: Create Visuals")
                              .Replace("@EmbedReportId", embeddingData.reportId)
                              .Replace("@EmbedUrl", embeddingData.embedUrl)
                              .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo31-EmbedNewReport-CreateVisual.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    public static void GeneratePaginatedReportPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetPaginatedReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithFirstPartyToken_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo21: Paginated Report - User Owns Data")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo21-EmbedPaginatedReportUserOwnsData.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    public static void GeneratePaginatedReportPageAppOnly(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiApiServiceManager.GetPaginatedReportEmbeddingDataAppOnly();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReport_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo22: Paginated Report - App Owns Data")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo22-EmbedPaginatedReportAppOwnsData.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }


  }
}
