using heimdall.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace heimdall.Configs;
public class ApplicationConfig
{
    /// <summary>
    /// SFTP Server endpoint
    /// </summary>
    public required string SftpEndpoint { get; set; }
    /// <summary>
    /// SFTP Server username
    /// </summary>
    public required string SftpUsername { get; set; }
    /// <summary>
    /// SFTP Server password
    /// </summary>
    public string? SftpPassword { get; set; }
    /// <summary>
    /// SFTP Server file encode
    /// 
    /// Default is UTF-8 (65001)
    /// </summary>
    public int SftpFileEncode { get; set; } = 65001;
    /// <summary>
    /// File upload path
    /// </summary>
    public string UploadPath { get; set; } = "";
    /// <summary>
    /// Upload file name
    /// 
    /// If empty, use blob name
    /// </summary>
    public string FileName { get; set; } = "";
    /// <summary>
    /// SFTP Server authentication method
    /// </summary>
    public AuthenticationMethod AuthenticationMethod { get; set; }
    /// <summary>
    /// SSH Private key
    /// </summary>
    public string? PrivateKey { get; set; }
    /// <summary>
    /// SSH Private key passphrase
    /// </summary>
    public string? PrivateKeyPassphrase { get; set; }
}

