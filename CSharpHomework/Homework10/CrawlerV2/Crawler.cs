using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerForm {
  class Crawler {
    public event Action<Crawler> CrawlerStopped;
    public event Action<Crawler, string, string> PageDownloaded;

    //所有已下载和待下载URL，key是URL，value表示是否下载成功
    private Dictionary<string, bool> urls = new Dictionary<string, bool>();

    //待下载队列
    private ConcurrentQueue<string> pending = new ConcurrentQueue<string>();

    //URL检测表达式，用于在HTML文本中查找URL
    private readonly string urlDetectRegex = @"(href|HREF)[]*=[]*[""'](?<url>[^""'#>]+)[""']";

    //URL解析表达式
    public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";
    public string HostFilter { get; set; } //主机过滤规则
    public string FileFilter { get; set; } //文件过滤规则
    public int MaxPage { get; set; } //最大下载数量
    public string StartURL { get; set; } //起始网址
    public Encoding HtmlEncoding { get; set; } //网页编码
    public Dictionary<string, bool> DownloadedPages { get => urls; } //已下载网页

    public Crawler() {
      MaxPage = 100;
      HtmlEncoding = Encoding.UTF8;
    }

        public void SimpleStart()
        {
            urls.Clear();
            ConcurrentQueue<string> newpending = new ConcurrentQueue<string>();
            pending = newpending;
            pending.Enqueue(StartURL);
            
            while (urls.Count < MaxPage && pending.Count > 0 )
            {

                int all = pending.Count;
                string url;
                if (!pending.TryDequeue(out url)) continue;
                
                if (url != null)
                {
                    DownLoad(url);
                }
                
            } 
            CrawlerStopped(this);
        }

    public void Start() {
      urls.Clear();
      ConcurrentQueue<string> newpending = new ConcurrentQueue<string>();
      pending = newpending;      
      //pending.Enqueue(StartURL);
            Task[] tasks = new Task[10000];
            bool[] flags = new bool[10000];
            for (int i = 0; i < 10000; i++) flags[i] = false;
            int tail = 1;
            DownLoad(StartURL);
            do
            {

                int all = pending.Count;
                string url;
                if (!pending.TryDequeue(out url)) continue;
                int num;
                if (flags[urls.Count - 1] == false)
                {
                    num = urls.Count - 1;
                    tail = urls.Count;
                }
                else
                {
                    num = tail;
                    tail++;
                }
                flags[num] = true;


                tasks[num] = new Task(() => DownLoad(url));
                if (url != null)
                {
                    tasks[num].Start();
                }


            } while (urls.Count < MaxPage && pending.Count > 0 && (tasks[tail - 1].IsCompleted | !tasks[tail - 1].IsCompleted));
      CrawlerStopped(this);
    }

    private void DownLoad(string url) {
            try {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = urls.Count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                urls[url] = true;
                PageDownloaded(this, url, "success");
                Parse(html, url);
            }catch(Exception E)
            {
                PageDownloaded(this, url, "  Error:" + E.Message);
            }
        }

    private void Parse(string html, string pageUrl) {
      var matches = new Regex(urlDetectRegex).Matches(html);
      foreach (Match match in matches) {
        string linkUrl = match.Groups["url"].Value;
        if (linkUrl == null || linkUrl == "") continue;
        linkUrl = FixUrl(linkUrl, pageUrl);//转绝对路径
        //解析出host和file两个部分，进行过滤
        Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
        string host = linkUrlMatch.Groups["host"].Value;
        string file = linkUrlMatch.Groups["file"].Value;
        if (file == "") file = "index.html";
        if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)) {
          if (!urls.ContainsKey(linkUrl)) {
            pending.Enqueue(linkUrl);
            urls.Add(linkUrl, false);
          }
        }


      }
    }


    //将相对路径转为绝对路径
    static private string FixUrl(string url, string pageUrl) {
      if (url.Contains("://")) {
        return url;
      }
      if (url.StartsWith("//")) {
        return "http:" + url;
      }
      if (url.StartsWith("/")) {
        Match urlMatch = Regex.Match(pageUrl, urlParseRegex);
        String site = urlMatch.Groups["site"].Value;
        return site.EndsWith("/") ? site + url.Substring(1) : site + url;
      }

      if (url.StartsWith("../")) {
        url = url.Substring(3);
        int idx = pageUrl.LastIndexOf('/');
        return FixUrl(url, pageUrl.Substring(0, idx));
      }

      if (url.StartsWith("./")) {
        return FixUrl(url.Substring(2), pageUrl);
      }

      int end = pageUrl.LastIndexOf("/");
      return pageUrl.Substring(0, end) + "/" + url;
    }
  }

   
}
