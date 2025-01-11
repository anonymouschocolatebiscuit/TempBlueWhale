/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：QyCustomMessageHandler.cs
    文件功能描述：自定义QyMessageHandler
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Sample.CommonService.QyMessageHandler;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

using Senparc.Weixin.MP.Helpers;//用于计算地理位置的GPS距离

namespace Senparc.Weixin.MP.Sample.CommonService.QyMessageHandlers
{
    public class QyCustomMessageHandler : QyMessageHandler<QyCustomMessageContext>
    {
        public QyCustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送了消息：" + requestMessage.Content;
            return responseMessage;
        }

        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageImage>();
            responseMessage.Image.MediaId = requestMessage.MediaId;
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您刚发送的图片如下：";
            return responseMessage;
        }

        public override QY.Entities.IResponseMessageBase DefaultResponseMessage(QY.Entities.IRequestMessageBase requestMessage)
        {

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这是一条没有找到合适回复信息的默认消息。";
            return responseMessage;

        }

        /// <summary>
        /// 处理位置请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {

            string openId = requestMessage.FromUserName.ToString();

            string toUser = requestMessage.ToUserName.ToString();

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();



            //	120.597725

            double distance = GpsHelper.Distance(120.597725, 31.312624, requestMessage.Location_Y, requestMessage.Location_X);
            string mi = "";
            if (distance > 1000)//如果大于一千米
            {
                mi = (distance / 1000).ToString("0.00") + "千米";
            }
            else
            {
                mi = distance.ToString("0") + "米";
            }

            responseMessage.Content = "openId:" + openId + " toUser:" + toUser;
                
                
                //string.Format("地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，标签：{3},距离我们：{4}",
                //            requestMessage.Location_X, requestMessage.Location_Y,
                //            requestMessage.Scale, requestMessage.Label, mi);


            return responseMessage;

            //var locationService = new LocationService();
            //var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
            //return responseMessage;


        }

        public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您刚才发送的是小视频";
            return responseMessage;
        }

        //这里是微信客户端（通过微信服务器）自动发送过来的位置信息
        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {

           
           

            //这里是微信客户端（通过微信服务器）自动发送过来的位置信息
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            // responseMessage.Content = "这里写什么都无所谓，比如：上帝爱你！";
            string openId = requestMessage.FromUserName.ToString();



            double distance = GpsHelper.Distance(120.597725, 31.312624, requestMessage.Longitude, requestMessage.Latitude);
            string mi = "";
            if (distance > 1000)//如果大于一千米
            {
                mi = (distance / 1000).ToString("0.00") + "千米";
            }
            else
            {
                mi = distance.ToString("0") + "米";
            }


            //companyDAL dal = new companyDAL();

            //int add = dal.UpdateLocation(openId,requestMessage.Latitude,requestMessage.Longitude);

            responseMessage.Content = string.Format("openId:" + openId + "您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1},距离我们：{2}",
                            requestMessage.Latitude.ToString(), requestMessage.Longitude,mi);

            return responseMessage;//这里也可以返回null（需要注意写日志时候null的问题）



            //var locationService = new LocationService();
            //var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
            //return responseMessage;


        }
    }
}
