﻿/*
 * Copyright (C) 2017. Huawei Technologies Co., LTD. All rights reserved.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of Apache License, Version 2.0.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * Apache License, Version 2.0 for more details.
 */
using Newtonsoft.Json;
using Smn.Http;
using Smn.Response.Subscription;
using Smn.Util;
using System;
using System.Text;

namespace Smn.Request.Subscription
{
    ///<summary> 
    /// list subscriptions request message
    /// author:zhangyx
    /// version:1.0.0
    ///</summary> 
    public class ListSubscriptionsByTopicRequest : AbstractRequest<ListSubscriptionsByTopicResponse>
    {
        /// <summary>
        /// offset
        /// </summary>
        private int offset = 0;

        /// <summary>
        /// limit
        /// </summary>
        private int limit = 100;

        /// <summary>
        /// topic urn
        /// </summary>
        private string topicUrn;

        [JsonIgnore]
        public string TopicUrn { get => topicUrn; set => topicUrn = value; }
        [JsonProperty("offset")]
        public int Offset { get => offset; set => offset = value; }
        [JsonProperty("limit")]
        public int Limit { get => limit; set => limit = value; }

        public override HttpMethod GetHttpMethod()
        {
            return HttpMethod.GET;
        }

        public override string GetUrl()
        {
            if (!ValidateUtil.ValidateOffset(offset))
            {
                throw new ArgumentException("offset is invalid");
            }

            if (!ValidateUtil.ValidateLimit(limit))
            {
                throw new ArgumentException("limit is invalid");
            }

            if (string.IsNullOrEmpty(topicUrn))
            {
                throw new ArgumentException("topic urn is null");
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(GetSmnServiceUrl());
            sb.Append(Constants.URL_DELIMITER).Append(Constants.V2).Append(Constants.URL_DELIMITER)
                    .Append(ProjectId).Append(Constants.URL_DELIMITER).Append(Constants.SMN_NOTIFICATIONS)
                    .Append(Constants.URL_DELIMITER).Append(Constants.TOPICS)
                    .Append(Constants.URL_DELIMITER).Append(topicUrn)
                    .Append(Constants.URL_DELIMITER).Append(Constants.SUBSCRIPTIONS);

            String parameters = GetQueryParameters(this);
            if (!string.IsNullOrEmpty(parameters))
            {
                sb.Append("?").Append(parameters);
            }
            return sb.ToString();
        }
    }
}
