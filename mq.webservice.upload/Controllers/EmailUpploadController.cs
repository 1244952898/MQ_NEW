﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using log4net;
using mq.application.common;
using mq.application.enumlib;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.webservice.upload.Controllers
{
    public class EmailUpploadController : Controller
    {
        ILog logger = LogManager.GetLogger(typeof(EmailUpploadController));
        private readonly IBgUserService _bgUserService;
        private readonly IBgUpFilesService _bgUpFilesService;

        public EmailUpploadController(IBgUserService bgUserService, IBgUpFilesService bgUpFilesService)
        {
            _bgUserService = bgUserService;
            _bgUpFilesService = bgUpFilesService;
        }
        // GET: EmailUppload
        public JsonResult SendFile()
        {
            string userid = CommonHelper.GetPostValue("key");
            string type = "EmailFile"; //CommonHelper.GetPostValue("type").ToString("PublicFile");

            OrignalTypeEnum saveType = OrignalTypeEnum.EmailFile;
            userid = LoginHelper.UserId.ToString();
            long lUserId = userid.ToLong(-1);
        
            if (string.IsNullOrEmpty(userid) || lUserId <= 0)
            {
                return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "非法请求！" });
            }
            T_BG_User bgUser = _bgUserService.GetUserById(userid.ToLong(-1));
            if (bgUser == null)
            {
                return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "请重新登录！" });
            }

            if (HttpContext.Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                    if (file.ContentLength <= 0)
                    {
                        return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "上传的附件为空！" });
                    }
                    //FileExtension[] fileExs = new FileExtension[] { FileExtension.DOC, FileExtension.DOCX, FileExtension.PDF };
                    //string fileExt = FileValidation.FileExtension(file, fileExs);
                    //if (string.IsNullOrEmpty(fileExt))
                    //{
                    //    return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "请上传WORD、PDF格式的文档！" });
                    //}

                    string fileExt = Path.GetExtension(file.FileName);
                    string originFileName = Regex.Replace(file.FileName, "." + fileExt, "." + fileExt, RegexOptions.IgnoreCase);
                    string savePath = OriginalSaveHelper.GetEmailSavePath(fileExt.Replace(".", ""), type);
                    if (string.IsNullOrEmpty(savePath))
                    {
                        return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "上传的路径获取失败！" });
                    }
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    string newFilePath = string.Format(@"{0}\{1}", savePath, file.FileName);
                    file.SaveAs(newFilePath);

                    T_BG_UpFiles bgUpFiles = new T_BG_UpFiles { filename = file.FileName, filehash = FileHelper.GetFileHash(newFilePath), userid = lUserId, fileoriginname = originFileName, filepath = newFilePath, ext = fileExt, addtime = DateTime.Now, type = saveType.ToInt(0) };
                    //long cnt = _bgUpFilesService.GetListByUserIdAndFileNameAndExt(originFileName, lUserId, fileExt, saveType.ToInt(0));
                    long reslt = _bgUpFilesService.Add(bgUpFiles);
                    if (reslt > 0)
                    {
                        return Json(new FileUploadEntity { ErrorCode = "00000", ErrorMessage = "上传成功", Attach = file.FileName, FilePath = bgUpFiles.filepath, FileType = fileExt });
                    }
                    else
                    {
                        return Json(new FileUploadEntity { ErrorCode = "10003", ErrorMessage = "服务端保存文件失败" });
                    }
                }
                catch (Exception)
                {
                    return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "服务端异常" });
                }
            }
            else
                return Json(new FileUploadEntity { ErrorCode = "10002", ErrorMessage = "请选择附件！" });
        }

        public JsonResult DelEmailFile()
        {
            string filePath = CommonHelper.GetPostValue("filePath");
            filePath = HttpUtility.UrlDecode(filePath);
            if (string.IsNullOrEmpty(filePath))
            {
                return Json(new FileUploadEntity { ErrorCode = "E0001", ErrorMessage = "未获得文件地址" });
            }
            T_BG_UpFiles upFiles = _bgUpFilesService.GetListByFilePathForEmail(filePath,LoginHelper.UserId);
            if (upFiles == null)
            {
                return Json(new FileUploadEntity { ErrorCode = "E0000", ErrorMessage = "未获得文件信息" });
            }
           
            string msg;
            bool result = FileHelper.DelFile(filePath, out msg);
            if (!result)
            {
                return Json(new FileUploadEntity { ErrorCode = "E0001", ErrorMessage = msg });
            }
            bool delResult = _bgUpFilesService.DelFile(upFiles);
            if (!delResult)
            {
                logger.ErrorFormat("邮件文件删除失败，Id：{0} \r\n  filename:{1} \r\n filepath:{2}", upFiles.Id, upFiles.filename, upFiles.filepath);
                return Json(new FileUploadEntity { ErrorCode = "E0001", ErrorMessage = "删除表里数据失败！" });
            }
            return Json(new FileUploadEntity { ErrorCode = "E0000", ErrorMessage = "删除成功" });
        }
    }
}