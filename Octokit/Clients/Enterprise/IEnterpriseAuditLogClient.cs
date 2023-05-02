using Octokit.Models.Request.Enterprise;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Audit Log API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log">Enterprise Audit Log API documentation</a> for more information.
    ///</remarks>
    public interface IEnterpriseAuditLogClient
    {
        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<AuditLogEvent>> GetAll(string enterprise);

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="request">Used to filter and sort the list of events returned</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<AuditLogEvent>> GetAll(string enterprise, AuditLogRequest request);

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="auditLogApiOptions">Options for changing the API response</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<AuditLogEvent>> GetAll(string enterprise, AuditLogApiOptions auditLogApiOptions);

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="request">Used to filter and sort the list of events returned</param>
        /// <param name="auditLogApiOptions">Options for changing the API response</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<AuditLogEvent>> GetAll(string enterprise, AuditLogRequest request, AuditLogApiOptions auditLogApiOptions);

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries as raw Json (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<object>> GetAllJson(string enterprise);

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries as raw Json (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="request">Used to filter and sort the list of events returned</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<object>> GetAllJson(string enterprise, AuditLogRequest request);

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries as raw Json (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="auditLogApiOptions">Options for changing the API response</param>
        /// <returns>The <see cref="AuditLogEvent"/> list.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<object>> GetAllJson(string enterprise, AuditLogApiOptions auditLogApiOptions);

        /// <summary>
        /// Gets GitHub Enterprise Audit Log Entries as raw Json (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log/#get-the-audit-log-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">Name of enterprise</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="auditLogApiOptions">Options for changing the API response</param>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<object>> GetAllJson(string enterprise, AuditLogRequest request, AuditLogApiOptions auditLogApiOptions);
    }
}
