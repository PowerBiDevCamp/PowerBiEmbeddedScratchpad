using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace PowerBiEmbeddedScratchpad.Services {

  class PageGenerator {

    #region "Internal implementation details"

    private static readonly string rootFolder = AppSettings.LocalPagesFolder;

    static PageGenerator() {
      Directory.CreateDirectory(rootFolder);
      Directory.CreateDirectory(rootFolder + "css");
      File.WriteAllText(rootFolder + "css/app.css", Properties.Resources.app_css);
      Directory.CreateDirectory(rootFolder + "css/img");
      File.WriteAllBytes(rootFolder + "favicon.ico", Properties.Resources.favicon_ico);
      File.WriteAllBytes(rootFolder + "css/img/loading.gif", Properties.Resources.loading_gif);
      File.WriteAllBytes(rootFolder + "css/img/loading2.gif", Properties.Resources.loading2_gif);
      File.WriteAllBytes(rootFolder + "css/img/loading3.gif", Properties.Resources.loading3_gif);
      File.WriteAllBytes(rootFolder + "css/img/background.jpg", Properties.Resources.background_jpg);
      File.WriteAllBytes(rootFolder + "css/img/menuicon.png", Properties.Resources.menuicon_png);
      Directory.CreateDirectory(rootFolder + "scripts");
      File.WriteAllText(rootFolder + "scripts/jquery.js", Properties.Resources.jquery_js);
      File.WriteAllText(rootFolder + "scripts/powerbi.min.js", Properties.Resources.powerbi_min_js);
      File.WriteAllText(rootFolder + "scripts/powerbi.js", Properties.Resources.powerbi_js);
      File.WriteAllText(rootFolder + "scripts/powerbi.js.map", Properties.Resources.powerbi_js_map);
      File.WriteAllText(rootFolder + "scripts/powerbi-report-authoring.js", Properties.Resources.powerbi_report_authoring_js);
      File.WriteAllText(rootFolder + "scripts/powerbi.js.map", Properties.Resources.powerbi_js_map);
    }

    static private void LaunchPageInBrowser(string pagePath) {
      Process process = new Process();
      process.StartInfo.UseShellExecute = true;
      process.StartInfo.FileName = "chrome";
      process.StartInfo.Arguments = " --new-tab " + pagePath;
      process.Start();
    }

    #endregion

    // demo 01
    public static void GenerateReportPageUserOwnsData(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingDataUserOwnsData();

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
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReport_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo02: Embed Report - App-Owns-Data")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken)
                                    .Replace("EmbedTokenType", "models.TokenType.Embed");



      // generate page file on local har drive
      string pagePath = rootFolder + "Demo02-EmbedReport-AppOwnsData.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 03
    public static void GenerateReportPageEditMode(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportEditMode_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo03: Edit Report")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken)
                                    .Replace("EmbedTokenType", "models.TokenType.Embed");


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo03-EmbedReport-EditMode.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 04
    public static void GenerateReportWithToolbarPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingDataForUser = PowerBiRestApiManager.GetReportEmbeddingDataUserOwnsData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithToolbar_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo04A: Toolbar with User-Owns-Data")
                                    .Replace("@EmbedReportId", embeddingDataForUser.reportId)
                                    .Replace("@EmbedUrl", embeddingDataForUser.embedUrl)
                                    .Replace("@EmbedToken", embeddingDataForUser.accessToken)
                                    .Replace("@TokenType", "models.TokenType.Aad");

      // generate page file on local har drive
      string pagePathForUser = rootFolder + "Demo04A-EmbedReport-CustomToolbar-User-Owns-Data.html";
      File.WriteAllText(pagePathForUser, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePathForUser);
      }

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingDataAllDatasets();

      // parse embedding data into page template
      htmlOutput = htmlSource.Replace("@AppName", "Demo04B: Toolbar with App-Owns-Data")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken)
                                    .Replace("@TokenType", "models.TokenType.Embed");      

      // generate page file on local har drive
      string pagePath = rootFolder + "Demo04B-EmbedReport-CustomToolbar-App-Owns-Data.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }

   

    }

    // demo 05
    public static void GenerateReportPageWithTransparentBackground(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithTransparentBackground_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo05: Transparent Background")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo05-EmbedReport-TransparentBackground.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 06
    public static void GenerateReportPageWithThemeLoading(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithThemeLoading_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo06: Apply Themes")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo06-EmbedReport-ApplyTheme.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 07
    public static void GenerateReportPageWithContrastMode(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithContrastMode_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo07: Contrast Mode")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo07-EmbedReport-ContrastMode.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 08
    public static void GenerateReportPageWithPhasedLoading(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithPhasedLoading_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo08: Phased Loading")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo08-EmbedReport-PhasedLoading.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 09
    public static void GenerateReportPageWithWhiteLabelLoading(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithWhiteLabelLoading_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo09: White Label Loading")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo09-EmbedReport-WhiteLabelLoading.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 10
    public static void GenerateReportPageWithBootstrapOptimization(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithBootstrap_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo10: Bootstrap Optimization")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo10-EmbedReport-BootstrapOptimization.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 11
    public static void GenerateReportVisualPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportVisual_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo11: Single Visual")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo11-EmbedReport-SingleVisual.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 12A
    public static void GenerateReportWithCustomLayout(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithCustomLayout_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo12A: Custom Layouts")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);

      // generate page file on local har drive
      string pagePath = rootFolder + "Demo12-EmbedReport-CustomLayout.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 12B
    public static void GenerateReportInspectorPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.ReportInspector_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo12B: Visual Inspector")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo12B-EmbedReport-VisualInspector.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 13
    public static void GenerateReportWithPageNavigation(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithPageNavigation_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo13: Page Navigation")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo13-EmbedReport-CustomPageNavigation.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 14
    public static void GenerateReportWithBookmarks(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithBookmarks_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo14: Bookmarks")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo14-EmbedReport-CustomBookmarksNavigation.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 15
    public static void GenerateReportWithBookmarkPlayMode(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithBookmarksPlayMode_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo15: Bookmarks Play Mode")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo15-EmbedReport-BookmarksPlayMode.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 16
    public static void GenerateReportWithBookmarkCarousel(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithBookmarkCarousel_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo16: Bookmark Carousel")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo16-EmbedReport-BookmarkCarousel.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 17
    public static void GenerateReportWithCustomFiltering(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithCustomFiltering_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo17: Custom Filtering")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo17-EmbedReport-CustomFiltering.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 18
    public static void GenerateReportWithContextMenusPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithContextMenus_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo18-Custom Visual Context Menus")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo18-EmbedReport-CustomVisualContextMenus.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 19
    public static void GenerateReportWithCustomExportCommand(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithCustomExportCommand_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo19 - Custom Export Command")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo19-EmbedReport-CustomExportCommand.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 20
    public static void GenerateReportWithHyperlinkNavigation(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithHyperlinkNavigation_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo20: HyperLikNavigation")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo20-EmbedReport-CustomHyperlinkNavigation.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 21
    public static void GenerateNewReportPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetNewReportEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedNewReport_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo21: New Report")
                                    .Replace("@EmbedWorkspaceId", embeddingData.workspaceId)
                                    .Replace("@EmbedDatasetId", embeddingData.datasetId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo21-EmbedNewReport-NoSaveAsSupport.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 22
    public static void GenerateNewReportPageWithSaveAsRedirect(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetNewReportEmbeddingDataUserOwnsData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedNewReportWithSaveAsRedirect_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo22: New Report with SaveAs")
                                    .Replace("@EmbedWorkspaceId", embeddingData.workspaceId)
                                    .Replace("@EmbedDatasetId", embeddingData.datasetId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.accessToken);


      // generate page file on local har drive
      string pagePath = rootFolder + "Demo22-EmbedNewReport-WithSaveAsRedirect.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }

    // demo 23
    public static void GenerateReportWithDynamicBinding(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingDataDynamicBinding();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithDynamicBinding_html;

      // create page 1
      string htmlOutput1 = htmlSource.Replace("@AppName", "Demo23A: Dynamic Binding - All Sales Data")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedToken", embeddingData.embedToken)
                                    .Replace("@DatasetId", embeddingData.dataset1Id);

      // generate page file on local har drive
      string pagePath1 = rootFolder + "Demo23A-EmbedReport-DynamicBinding-AllSalesData.html";
      File.WriteAllText(pagePath1, htmlOutput1);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath1);
      }

      // create page 2
      string htmlOutput2 = htmlSource.Replace("@AppName", "Demo23B: Dynamic Binding - New England")
                                 .Replace("@EmbedReportId", embeddingData.reportId)
                                 .Replace("@EmbedUrl", embeddingData.embedUrl)
                                 .Replace("@EmbedToken", embeddingData.embedToken)
                                 .Replace("@DatasetId", embeddingData.dataset2Id);

      // generate page file on local har drive
      string pagePath2 = rootFolder + "Demo23B-EmbedReport-DynamicBinding-NewEngland.html";
      File.WriteAllText(pagePath2, htmlOutput2);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath2);
      }


      // create page 3
      string htmlOutput3 = htmlSource.Replace("@AppName", "Demo23C: Dynamic Binding - Gulf States")
                                 .Replace("@EmbedReportId", embeddingData.reportId)
                                 .Replace("@EmbedUrl", embeddingData.embedUrl)
                                 .Replace("@EmbedToken", embeddingData.embedToken)
                                 .Replace("@DatasetId", embeddingData.dataset3Id);

      // generate page file on local har drive
      string pagePath3 = rootFolder + "Demo23C-EmbedReport-DynamicBinding-GulfStates.html";
      File.WriteAllText(pagePath3, htmlOutput3);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath3);
      }

    }

    // demo 24
    public static void GenerateReportWithRls(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportWithRlsEmbeddingData();

      // parse embedding data into page template
      string htmlSource = Properties.Resources.EmbedReportWithRLS_html;
      string htmlOutput = htmlSource.Replace("@AppName", "Demo24: Row Level Security")
                                    .Replace("@EmbedReportId", embeddingData.reportId)
                                    .Replace("@EmbedUrl", embeddingData.embedUrl)
                                    .Replace("@EmbedTokenAllData", embeddingData.embedTokenAllData)
                                    .Replace("@EmbedTokenWesternSales", embeddingData.embedTokenWesternSales)
                                    .Replace("@EmbedTokenCentralSales", embeddingData.embedTokenCentralSales)
                                    .Replace("@EmbedTokenEasternSales", embeddingData.embedTokenEasternSales)
                                    .Replace("@EmbedTokenCombo", embeddingData.embedTokenCombo);

      // generate page file on local har drive
      string pagePath = rootFolder + "Demo24-EmbedReport-RowLevelSecurity.html";
      File.WriteAllText(pagePath, htmlOutput);

      // launch page in browser if requested
      if (LaunchInBrowser) {
        LaunchPageInBrowser(pagePath);
      }
    }



    // demo xx
    public static void GenerateReportPageWithLocalizedLanguage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

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

    // demo xx
    public static void GenerateReportWithEventLogging(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

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


  
    //// demo 29
    public static void GenerateReportWithCorrelationId(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportWithAuditingData();

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


    // demo 31
    public static void GenerateNewReportCreateVisual(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetReportEmbeddingData();

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

  


    // left over
    // demo 03
    public static void GenerateDashboardPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetDashboardEmbeddingData();

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
      var embeddingData = PowerBiRestApiManager.GetDashboardTileEmbeddingData();

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
      var embeddingData = PowerBiRestApiManager.GetQnaEmbeddingData();

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

  
    public static void GeneratePaginatedReportPage(bool LaunchInBrowser = true) {

      // get Power BI embedding data
      var embeddingData = PowerBiRestApiManager.GetPaginatedReportEmbeddingData();

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
      var embeddingData = PowerBiRestApiManager.GetPaginatedReportEmbeddingDataAppOnly();

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
