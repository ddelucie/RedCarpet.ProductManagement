/*******************************************************************************
 * Copyright 2009-2015 Amazon Services. All Rights Reserved.
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 *
 * You may not use this file except in compliance with the License. 
 * You may obtain a copy of the License at: http://aws.amazon.com/apache2.0
 * This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 * CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 * specific language governing permissions and limitations under the License.
 *******************************************************************************
 * Send Test Notification To Destination Response
 * API Version: 2013-07-01
 * Library Version: 2015-06-18
 * Generated: Thu Jun 18 19:27:11 GMT 2015
 */


using System;
using System.Xml;
using MWSClientCsRuntime;

namespace MWSSubscriptionsService.Model
{
    public class SendTestNotificationToDestinationResponse : AbstractMwsObject, IMWSResponse
    {

        private SendTestNotificationToDestinationResult _sendTestNotificationToDestinationResult;
        private ResponseMetadata _responseMetadata;
        private ResponseHeaderMetadata _responseHeaderMetadata;

        /// <summary>
        /// Gets and sets the SendTestNotificationToDestinationResult property.
        /// </summary>
        public SendTestNotificationToDestinationResult SendTestNotificationToDestinationResult
        {
            get { return this._sendTestNotificationToDestinationResult; }
            set { this._sendTestNotificationToDestinationResult = value; }
        }

        /// <summary>
        /// Sets the SendTestNotificationToDestinationResult property.
        /// </summary>
        /// <param name="sendTestNotificationToDestinationResult">SendTestNotificationToDestinationResult property.</param>
        /// <returns>this instance.</returns>
        public SendTestNotificationToDestinationResponse WithSendTestNotificationToDestinationResult(SendTestNotificationToDestinationResult sendTestNotificationToDestinationResult)
        {
            this._sendTestNotificationToDestinationResult = sendTestNotificationToDestinationResult;
            return this;
        }

        /// <summary>
        /// Checks if SendTestNotificationToDestinationResult property is set.
        /// </summary>
        /// <returns>true if SendTestNotificationToDestinationResult property is set.</returns>
        public bool IsSetSendTestNotificationToDestinationResult()
        {
            return this._sendTestNotificationToDestinationResult != null;
        }

        /// <summary>
        /// Gets and sets the ResponseMetadata property.
        /// </summary>
        public ResponseMetadata ResponseMetadata
        {
            get { return this._responseMetadata; }
            set { this._responseMetadata = value; }
        }

        /// <summary>
        /// Sets the ResponseMetadata property.
        /// </summary>
        /// <param name="responseMetadata">ResponseMetadata property.</param>
        /// <returns>this instance.</returns>
        public SendTestNotificationToDestinationResponse WithResponseMetadata(ResponseMetadata responseMetadata)
        {
            this._responseMetadata = responseMetadata;
            return this;
        }

        /// <summary>
        /// Checks if ResponseMetadata property is set.
        /// </summary>
        /// <returns>true if ResponseMetadata property is set.</returns>
        public bool IsSetResponseMetadata()
        {
            return this._responseMetadata != null;
        }

        /// <summary>
        /// Gets and sets the ResponseHeaderMetadata property.
        /// </summary>
        public ResponseHeaderMetadata ResponseHeaderMetadata
        {
            get { return this._responseHeaderMetadata; }
            set { this._responseHeaderMetadata = value; }
        }

        /// <summary>
        /// Sets the ResponseHeaderMetadata property.
        /// </summary>
        /// <param name="responseHeaderMetadata">ResponseHeaderMetadata property.</param>
        /// <returns>this instance.</returns>
        public SendTestNotificationToDestinationResponse WithResponseHeaderMetadata(ResponseHeaderMetadata responseHeaderMetadata)
        {
            this._responseHeaderMetadata = responseHeaderMetadata;
            return this;
        }

        /// <summary>
        /// Checks if ResponseHeaderMetadata property is set.
        /// </summary>
        /// <returns>true if ResponseHeaderMetadata property is set.</returns>
        public bool IsSetResponseHeaderMetadata()
        {
            return this._responseHeaderMetadata != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _sendTestNotificationToDestinationResult = reader.Read<SendTestNotificationToDestinationResult>("SendTestNotificationToDestinationResult");
            _responseMetadata = reader.Read<ResponseMetadata>("ResponseMetadata");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("SendTestNotificationToDestinationResult", _sendTestNotificationToDestinationResult);
            writer.Write("ResponseMetadata", _responseMetadata);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonservices.com/schema/Subscriptions/2013-07-01", "SendTestNotificationToDestinationResponse", this);
        }

        public SendTestNotificationToDestinationResponse() : base()
        {
        }
    }
}
