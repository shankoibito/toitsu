/******************************************************************************
 Disclaimer Notice:
 This file is provided as is with no warranties of any kind and is
 provided without any obligation on Fork Particle, Inc. to assist in 
 its use or modification. Fork Particle, Inc. will not, under any
 circumstances, be liable for any lost revenue or other damages arising 
 from the use of this file.
 
 (c) Copyright 2017 Fork Particle, Inc. All rights reserved.
******************************************************************************/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if (UNITY_EDITOR) 
public class PSBImporter : AssetImporter
{	
	public const string PSBExtension = ".psb";
	public const string XMLExtension = ".xml";
	public const string AssetExtension = ".asset";
	public const string PrefabExtension = ".prefab";
	public const string XMLExtensionPrefix = "PSK";

	public static void Import(string assetPath)
	{
		GameObject gameObject = EditorUtility.CreateGameObjectWithHideFlags("", HideFlags.HideInHierarchy);
		string prefabFilePath = GetPrefabPath(assetPath);
		GameObject prefab = PrefabUtility.CreatePrefab(prefabFilePath, gameObject, ReplacePrefabOptions.ReplaceNameBased);
				 
		prefab.AddComponent<ForkParticleEffect>();

        MeshRenderer mesh = prefab.AddComponent<MeshRenderer>();
        mesh.receiveShadows = false;
        mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mesh.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        mesh.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
		mesh.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;

        prefab.AddComponent<MeshFilter>();

		AssetDatabase.SaveAssets();

		CreateXML(assetPath);
		GameObject.DestroyImmediate(gameObject);
	}

	public static void Delete(string assetPath)
	{
		string prefabFilePath = GetPrefabDeletionPath(assetPath);
		AssetDatabase.DeleteAsset(prefabFilePath);

		string xmlFilePath = GetXMLDeletionPath(assetPath);
		AssetDatabase.DeleteAsset(xmlFilePath);
	}

	public static bool IsPSBFile(string assetPath)
	{
		return assetPath.EndsWith(PSBExtension, StringComparison.OrdinalIgnoreCase);
	}

	public static string GetAssetPath(string assetPath)
	{
		return Path.ChangeExtension(assetPath, AssetExtension);
	}

	public static string GetPrefabPath(string assetPath)
	{
		assetPath = assetPath.Remove(assetPath.IndexOf('.'));
		assetPath = assetPath + "_prefab" + PrefabExtension;
		return assetPath;
		//return Path.ChangeExtension(assetPath, PrefabExtension);
	}

	public static void CreateXML(string assetPath)
	{
		string tempAssetPath = assetPath;
		
		tempAssetPath = tempAssetPath.Remove(tempAssetPath.LastIndexOf('.'));
		tempAssetPath += "_" + XMLExtensionPrefix + XMLExtension;

		FileUtil.CopyFileOrDirectory(assetPath, tempAssetPath);
		AssetDatabase.Refresh();
	}

	public static string GetPrefabDeletionPath(string assetPath)
	{
		assetPath = assetPath.Remove(assetPath.IndexOf('.'));
		assetPath = assetPath + "_prefab" + PrefabExtension;
		return assetPath;
	}

	public static string GetXMLDeletionPath(string assetPath)
	{
		assetPath = assetPath.Remove(assetPath.IndexOf('.'));
		assetPath = assetPath + "_" + XMLExtensionPrefix + XMLExtension;
		return assetPath;
	}
}
#endif