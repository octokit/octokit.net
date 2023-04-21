﻿using Octokit.Models.Request.Enterprise;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Admin Stats API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
    ///</remarks>
    public class EnterpriseAuditLogClient : ApiClient, IEnterpriseAuditLogClient
    {
        public EnterpriseAuditLogClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries (must be Site Admin user).  Note: Defaults to 100 entries per page (max count).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [ManualRoute("GET", "/enterprise/audit-log")]
        public Task<IReadOnlyList<AuditLogEvent>> GetAll(string enterprise)
        {
            Ensure.ArgumentNotNull(enterprise, nameof(enterprise));

            return GetAll(enterprise, new AuditLogRequest(), new ApiOptions() { PageSize = 100});
        }

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries (must be Site Admin user). Note: Defaults to 100 entries per page (max count).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [ManualRoute("GET", "/enterprise/audit-log")]
        public Task<IReadOnlyList<AuditLogEvent>> GetAll(string enterprise, AuditLogRequest request)
        {
            Ensure.ArgumentNotNull(enterprise, nameof(enterprise));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(enterprise, request, new ApiOptions() { PageSize = 100 });
        }

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [ManualRoute("GET", "/enterprise/audit-log")]
        public Task<IReadOnlyList<AuditLogEvent>> GetAll(string enterprise, ApiOptions options)
        {
            Ensure.ArgumentNotNull(enterprise, nameof(enterprise));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAll(enterprise, new AuditLogRequest(), options);
        }

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="request">Used to filter and sort the list of events returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [ManualRoute("GET", "/enterprise/audit-log")]
        public Task<IReadOnlyList<AuditLogEvent>> GetAll(string enterprise, AuditLogRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(enterprise, nameof(enterprise));

            return ApiConnection.GetAll<AuditLogEvent>(ApiUrls.EnterpriseAuditLog(enterprise), request.ToParametersDictionary(), null, options, (r) =>
            {
                if (r is string body)
                    r = body.Replace("_document_id", "document_id").Replace("@timestamp", "timestamp");
                return r;
            });
        }

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries as raw Json (must be Site Admin user). Note: Defaults to 100 entries per page (max count).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [ManualRoute("GET", "/enterprise/audit-log")]
        public Task<IReadOnlyList<object>> GetAllJson(string enterprise)
        {
            Ensure.ArgumentNotNull(enterprise, nameof(enterprise));

            return GetAllJson(enterprise, new AuditLogRequest(), new ApiOptions() { PageSize = 100 });
        }

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries as raw Json (must be Site Admin user). Note: Defaults to 100 entries per page (max count).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="request">Used to filter and sort the list of events returned</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [ManualRoute("GET", "/enterprise/audit-log")]
        public Task<IReadOnlyList<object>> GetAllJson(string enterprise, AuditLogRequest request)
        {
            Ensure.ArgumentNotNull(enterprise, nameof(enterprise));

            return GetAllJson(enterprise, request, new ApiOptions() { PageSize = 100 });
        }

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries as raw Json (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [ManualRoute("GET", "/enterprise/audit-log")]
        public Task<IReadOnlyList<object>> GetAllJson(string enterprise, ApiOptions options)
        {
            Ensure.ArgumentNotNull(enterprise, nameof(enterprise));

            return GetAllJson(enterprise, new AuditLogRequest(), options);
        }

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries as raw Json (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="request">Used to filter and sort the list of events returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [ManualRoute("GET", "/enterprise/audit-log")]
        public Task<IReadOnlyList<object>> GetAllJson(string enterprise, AuditLogRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(enterprise, nameof(enterprise));

            return ApiConnection.GetAll<object>(ApiUrls.EnterpriseAuditLog(enterprise), request.ToParametersDictionary(), options);
        }
    }
}
