﻿using System.Web.Optimization;
using mq.application.webmvc;

namespace MQWebSite
{
    public class BundleConfig
    {
        private static string _sourchPath = string.Empty;
        private static string SourchPath
        {
            get
            {
                if (string.IsNullOrEmpty(_sourchPath))
                    _sourchPath = DomainUrlHelper.SourchPath;
                if (!string.IsNullOrWhiteSpace(_sourchPath))
                {
                    _sourchPath = _sourchPath.TrimEnd(new char[] { '/' });
                }
                return BundleConfig._sourchPath;
            }
        }
        private static string _sourchTempPath = string.Empty;
        private static string SourchTempPath
        {
            get
            {
                if (string.IsNullOrEmpty(_sourchTempPath))
                    _sourchTempPath = DomainUrlHelper.SourchTempPath;
                if (!string.IsNullOrWhiteSpace(_sourchTempPath))
                {
                    _sourchTempPath = _sourchTempPath.TrimEnd(new char[] { '/' });
                }
                return BundleConfig._sourchTempPath;
            }
        }

        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string sourchPath = SourchPath;
            string sourceTempPath = SourchTempPath;
            BundleTable.EnableOptimizations = true;//捆绑压缩CDN资源 
            bundles.UseCdn = true;

            #region StyleBundle
            bundles.Add(new StyleBundle("~/css/BuildStyle", string.Format("{0}/css/BuildStyle.css", sourchPath)).Include("~/css/BuildStyle.css"));
            bundles.Add(new StyleBundle("~/css/style", string.Format("{0}/css/style.css", sourchPath)).Include("~/css/style.css"));
  
            bundles.Add(new StyleBundle("~/css/backstage", string.Format("{0}/css/backstage.css", sourchPath)).Include("~/css/backstage.css"));

            bundles.Add(new StyleBundle("~/css/mainerror", string.Format("{0}/css/mainerror.css", sourchPath)).Include("~/css/mainerror.css"));
            bundles.Add(new StyleBundle("~/css/ie8", string.Format("{0}/css/ie8.css", sourchPath)).Include("~/css/ie8.css"));
            bundles.Add(new StyleBundle("~/css/tipsy", string.Format("{0}/css/tipsy.css", sourchPath)).Include("~/css/tipsy.css"));
            bundles.Add(new StyleBundle("~/css/buttonsCSS", string.Format("{0}/css/buttonsCSS.css", sourchPath)).Include("~/css/buttonsCSS.css"));
            bundles.Add(new StyleBundle("~/css/UploadMessageCss", string.Format("{0}/css/UploadMessageCss.css", sourchPath)).Include("~/css/UploadMessageCss.css"));

            #region employeebg
            bundles.Add(new StyleBundle("~/css/employbg/ActiveFile/AddFile", string.Format("{0}/css/employbg/ActiveFile/AddFile.css", sourchPath)).Include("~/css/employbg/ActiveFile/AddFile.css"));
            bundles.Add(new StyleBundle("~/css/employbg/ActiveFile/FileList", string.Format("{0}/css/employbg/ActiveFile/FileList.css", sourchPath)).Include("~/css/employbg/ActiveFile/FileList.css"));
            #endregion

            #endregion

            #region ScriptBundle
            bundles.Add(new ScriptBundle("~/js/jquery", string.Format("{0}/js/jquery-3.2.1.min.js", sourchPath)).Include("~/js/jquery-3.2.1.min.js"));
            bundles.Add(new ScriptBundle("~/js/building", string.Format("{0}/js/preloader.js", sourchPath)).Include("~/js/preloader.js"));
            bundles.Add(new ScriptBundle("~/js/css_browser_selector", string.Format("{0}/js/css_browser_selector.js", sourchPath)).Include("~/js/css_browser_selector.js"));
            bundles.Add(new ScriptBundle("~/js/plax", string.Format("{0}/js/plax.js", sourchPath)).Include("~/js/plax.js"));
            bundles.Add(new ScriptBundle("~/js/jquery.spritely", string.Format("{0}/js/jquery.spritely-0.6.1.js", sourchPath)).Include("~/js/jquery.spritely-0.6.1.js"));
            bundles.Add(new ScriptBundle("~/js/jquery-animate-css-rotate-scale", string.Format("{0}/js/jquery-animate-css-rotate-scale.js", sourchPath)).Include("~/js/jquery-animate-css-rotate-scale.js"));
            bundles.Add(new ScriptBundle("~/js/script", string.Format("{0}/js/script.js", sourchPath)).Include("~/js/script.js"));

            bundles.Add(new ScriptBundle("~/js/NSW_Index", string.Format("{0}/js/NSW_Index.js", sourchPath)).Include("~/js/NSW_Index.js"));
            bundles.Add(new ScriptBundle("~/js/rollup.min", string.Format("{0}/js/rollup.min.js", sourchPath)).Include("~/js/rollup.min.js"));

            bundles.Add(new ScriptBundle("~/js/jquery.flexslider-min", string.Format("{0}/js/jquery.flexslider-min.js", sourchPath)).Include("~/js/jquery.flexslider-min.js"));
            bundles.Add(new ScriptBundle("~/js/jquery.placeholder", string.Format("{0}/js/jquery.placeholder.js", sourchPath)).Include("~/js/jquery.placeholder.js"));
            bundles.Add(new ScriptBundle("~/libs/layer/layer", string.Format("{0}/libs/layer/layer.js", sourchPath)).Include("~/libs/layer/layer.js"));

          

            bundles.Add(new ScriptBundle("~/js/jsencrypt.min", string.Format("{0}/js/jsencrypt.min.js", sourchPath)).Include("~/js/jsencrypt.min.js"));




            bundles.Add(new StyleBundle("~/public/header-and-footer", string.Format("{0}/css/header-and-footer.css", sourchPath)).Include("~/css/header-and-footer.css"));
            bundles.Add(new StyleBundle("~/public/main", string.Format("{0}/css/main.css", sourchPath)).Include("~/css/main.css"));
            bundles.Add(new StyleBundle("~/public/platform", string.Format("{0}/css/platform.css", sourchPath)).Include("~/css/platform.css"));
            bundles.Add(new StyleBundle("~/public/public", string.Format("{0}/css/public.css", sourchPath)).Include("~/css/public.css"));

            bundles.Add(new StyleBundle("~/css/jquery.bs_pagination.min", string.Format("{0}/libs/pagination/jquery.bs_pagination.min.css", sourchPath)).Include("~/libs/pagination/jquery.bs_pagination.min.css"));


            bundles.Add(new StyleBundle("~/css/bootstrap_pagination", string.Format("{0}/css/bootstrap-pagination.css", sourchPath)).Include("~/css/bootstrap-pagination.css"));
            bundles.Add(new ScriptBundle("~/js/vue", string.Format("{0}/js/vue.min.js", sourchPath)).Include("~/js/vue.min.js"));
            bundles.Add(new ScriptBundle("~/js/bootstrap", string.Format("{0}/js/bootstrap.js", sourchPath)).Include("~/js/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/js/respond", string.Format("{0}/js/respond.js", sourchPath)).Include("~/js/respond.js"));
            bundles.Add(new ScriptBundle("~/libs/pagination", string.Format("{0}/libs/pagination/jquery.bs_pagination.min.js", sourchPath)).Include("~/libs/pagination/jquery.bs_pagination.min.js"));

            bundles.Add(new ScriptBundle("~/libs/zh.min", string.Format("{0}/libs/pagination/localization/zh.min.js", sourchPath)).Include("~/libs/pagination/localization/zh.min.js"));

            #region 5.遮罩层(layer)
            bundles.Add(new ScriptBundle("~/libs/layer/js", string.Format("{0}/libs/layer/layer.js", sourchPath)).Include("~/libs/layer/layer.js"));
            #endregion
            #region 6.laydate
            bundles.Add(new ScriptBundle("~/libs/laydate/js", string.Format("{0}/libs/laydate/laydate.js", sourchPath)).Include("~/libs/laydate/laydate.js"));
            #endregion
            #region 7.divSelect
            bundles.Add(new ScriptBundle("~/js/divSelect", string.Format("{0}/js/divSelect.js", sourchPath)).Include("~/js/divSelect.js"));
            #endregion
            #region 8.jquery.movetabletr
            bundles.Add(new ScriptBundle("~/js/movetabletr", string.Format("{0}/js/jquery.movetabletr.js", sourchPath)).Include("~/js/jquery.movetabletr.js"));
            #endregion

            bundles.Add(new ScriptBundle("~/js/custom-scripts", string.Format("{0}/js/custom-scripts.js", sourchPath)).Include("~/js/custom-scripts.js"));
            bundles.Add(new ScriptBundle("~/js/jquery.tipsy", string.Format("{0}/js/jquery.tipsy.js", sourchPath)).Include("~/js/jquery.tipsy.js"));

            //AngularJs
            bundles.Add(new ScriptBundle("~/js/AngularJs/angular", string.Format("{0}/js/AngularJs/angular.min.js", sourchPath)).Include("~/js/AngularJs/angular.min.js"));
            #endregion

            #region libs

            //1.webupload
            bundles.Add(new StyleBundle("~/libs/webuploader/webuploaderCss", string.Format("{0}/libs/webuploader/webuploader.css", sourchPath)).Include("~/libs/webuploader/webuploader.css"));
            bundles.Add(new ScriptBundle("~/libs/webuploader/webuploaderJs", string.Format("{0}/libs/webuploader/webuploader.js", sourchPath)).Include("~/libs/webuploader/webuploader.js"));
            bundles.Add(new StyleBundle("~/libs/webuploader/DemoCss", string.Format("{0}/libs/webuploader/DemoCss.css", sourchPath)).Include("~/libs/webuploader/DemoCss.css"));

            //2.datetimepicker
            bundles.Add(new ScriptBundle("~/libs/datetimepicker/jquery.datetimepickercjs", string.Format("{0}/libs/datetimepicker/build/jquery.datetimepicker.full.js", sourchPath)).Include("~/libs/datetimepicker/build/jquery.datetimepicker.full.js"));

            bundles.Add(new StyleBundle("~/libs/datetimepicker/jquery.datetimepickercss", string.Format("{0}/libs/datetimepicker/jquery.datetimepicker.css", sourchPath)).Include("~/libs/datetimepicker/jquery.datetimepicker.css"));

            //3.Ueditor
            bundles.Add(new ScriptBundle("~/libs/ueditor/ueditor.config", string.Format("{0}/libs/ueditor/ueditor.config.js", sourchPath)).Include("~/libs/ueditor/ueditor.config.js"));

            bundles.Add(new ScriptBundle("~/libs/ueditor/ueditor.all", string.Format("{0}/libs/ueditor/ueditor.all.js", sourchPath)).Include("~/libs/ueditor/ueditor.all.js"));

            //4.layui
            bundles.Add(new StyleBundle("~/libs/layui/layui/css/layui", string.Format("{0}/libs/layui/layui/css/layui.css", sourchPath)).Include("~/libs/layui/layui/css/layui.css"));
            bundles.Add(new ScriptBundle("~/libs/layui/layui/layui", string.Format("{0}/libs/layui/layui/layui.js", sourchPath)).Include("~/libs/layui/layui/layui.js"));

            //5.bootstrap3.3.7
            bundles.Add(new StyleBundle("~/libs/bootstrap3.3.7/css/bootstrap", string.Format("{0}/libs/bootstrap3.3.7/css/bootstrap.min.css", sourchPath)).Include("~/libs/bootstrap3.3.7/css/bootstrap.min.css"));
            bundles.Add(new ScriptBundle("~/libs/bootstrap3.3.7/js/bootstrap", string.Format("{0}/libs/bootstrap3.3.7/js/bootstrap.min.js", sourchPath)).Include("~/libs/bootstrap3.3.7/js/bootstrap.min.js"));

            //6 uploadify
            bundles.Add(new StyleBundle("~/libs/uploadify/uploadify", string.Format("{0}/libs/uploadify/uploadify.css", sourchPath)).Include("~/libs/uploadify/uploadify.css"));
            bundles.Add(new ScriptBundle("~/libs/uploadify/jquery.uploadify", string.Format("{0}/libs/uploadify/jquery.uploadify.min.js", sourchPath)).Include("~/libs/uploadify/jquery.uploadify.min.js"));

            bundles.Add(new ScriptBundle("~/libs/uploadify/jquery.uploadify", string.Format("{0}/libs/uploadify/jquery.uploadify.min.js", sourchPath)).Include("~/libs/uploadify/jquery.uploadify.min.js"));

            //7 angular-1.6.5
            bundles.Add(new ScriptBundle("~/libs/angular-1.6.5/angular", string.Format("{0}/libs/angular-1.6.5/angular.min.js", sourchPath)).Include("~/libs/angular-1.6.5/angular.min.js"));

            //layui 2.0
            bundles.Add(new StyleBundle("~/libs/layui-2.0/css/layui", string.Format("{0}/libs/layui-2.0/css/layui.css", sourchPath)).Include("~/libs/layui-2.0/css/layui.css"));
            bundles.Add(new ScriptBundle("~/libs/layui-2.0/layui", string.Format("{0}/libs/layui-2.0/layui.js", sourchPath)).Include("~/libs/layui-2.0/layui.js"));
			bundles.Add(new ScriptBundle("~/libs/layui-2.0/layui.all", string.Format("{0}/libs/layui-2.0/layui.all.js", sourchPath)).Include("~/libs/layui-2.0/layui.all.js"));

			//jquery.cookie-1.4.1.min.js
			bundles.Add(new ScriptBundle("~/Scripts/jquery.cookie-1.4.1.min", string.Format("{0}/Scripts/jquery.cookie-1.4.1.min.js", sourchPath)).Include("~/Scripts/jquery.cookie-1.4.1.min.js"));
			#endregion
		}
	}
}