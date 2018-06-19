using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public interface ICacheService
{
	bool Exist(string key);
	
	void InvalidateAll();
	void InvalidateKey(string key);
	
	void Save(string key, byte[] data);
	byte[] Get(string key);
}


public class BinaryCacheService : ICacheService
{
	private	Dictionary<string, CacheItem> _data = new Dictionary<string, CacheItem>();
	private string _cachePath;
	
	public BinaryCacheService()
	{
		_cachePath = Application.temporaryCachePath;
		var dirInfo = new DirectoryInfo(Application.temporaryCachePath);
		foreach (var fileInfo in dirInfo.GetFiles().Where(p => p.Extension.Contains("cache")))
		{
			_data.Add(fileInfo.Name.Replace(".cache", ""), new CacheItem() {CachedStatus = CachedStatus.Ready});
		}
	}

	private string CacheHash(string key)
	{
        using (var md5 = MD5.Create())
		{
			return GetMd5Hash(md5, key);
		}
	}
	
	private string GetMd5Hash(MD5 md5Hash, string input)
	{
		var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
		var sBuilder = new StringBuilder();
		
		for (var i = 0; i < data.Length; i++)
		{
			sBuilder.Append(data[i].ToString("x2"));
		}

		return sBuilder.ToString();
	}
	
	private string CacheFileName(string key)
	{
		return Path.Combine(_cachePath, CacheHash(key) + ".cache");
	}
	
	public bool Exist(string key)
	{
		return _data.ContainsKey(CacheHash(key));
	}

	public void InvalidateAll()
	{
		foreach (var dataItem in _data)
		{
			if (File.Exists(CacheFileName(dataItem.Key)))
			{
				File.Delete(CacheFileName(dataItem.Key));
			}
		}
		
		_data.Clear();
	}

	public void InvalidateKey(string key)
	{
		if (File.Exists(CacheFileName(key)))
		{
			File.Delete(CacheFileName(key));
		}

		_data.Remove(CacheHash(key));
	}
	
	public void Save(string key, byte[] data)
	{
		if (Exist(key))
		{
			InvalidateKey(key);
		}

		try
		{
			File.WriteAllBytes(CacheFileName(key), data);
			_data.Add(CacheHash(key), new CacheItem()
			{
				Payload = data,
				CachedStatus = CachedStatus.Loaded
			});
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
		}
	
	}

	public byte[] Get(string key)
	{
		if (Exist(key))
		{
			var cacheItem = _data[CacheHash(key)];
			if (cacheItem.CachedStatus == CachedStatus.Loaded)
			{
				return cacheItem.Payload;
			}

			cacheItem.Payload = File.ReadAllBytes(CacheFileName(key));
			cacheItem.CachedStatus = CachedStatus.Loaded;
			return cacheItem.Payload;
		}
		
		
		throw new UnityException(string.Format("Cache with key {0} not exist", key));
	}
}

public enum CachedStatus
{
	Ready,
	Loaded
}

public class CacheItem
{
	public byte[] Payload;
	public CachedStatus CachedStatus;
}