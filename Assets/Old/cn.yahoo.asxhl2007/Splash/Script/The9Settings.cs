using UnityEngine;
using System.Collections;
using System;

public class The9Settings : MonoBehaviour {
	
	public Appstore mAppstore = Appstore.Default;
	
	public static string appstoreName;
	
	public static Appstore appstore;
	
	public enum Appstore {
		Default,
		estore,
		wostore,
		mm,
		wanpu,
		gfanmarket,
		gfanforum,
		hiapk,
		goapk,
		appchina,
		meizumarket,
		meizuforum,
		zhihuiyun,
		the9web,
		the9app,
		shuaji,
		eoemarket,
		gfanrom,
		gfanpartnera,
		gfanpartnerb,
		tgbus,
		appoole,
		crossmo,
		coolmart,
		htc,
		nduoa,
		motorola
	}
	
	void Awake() {
		appstore = mAppstore;
		if(mAppstore == Appstore.Default){
			appstoreName = "default";
		} else {
			appstoreName = Enum.GetName(typeof(Appstore), mAppstore);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*
	public class Appstore{
		
		// 渠道名（商城名）
		private string sAppstore;

		// 私有构造保证值域的封闭性
		public Appstore(string ssAppstore) {
			sAppstore = ssAppstore;
		}

		//public static readonly Appstore Default = new Appstore("default");
		//public static readonly Appstore MM = new Appstore("mm");
		//public static readonly Appstore EStore = new Appstore("estore");
		//public static readonly Appstore WoStore = new Appstore("wostore");
	   
		// 提供重载的"=="操作符，使用sAppstore来判断是否是相同的Appstore类型
		//public static bool operator ==(Appstore op1, Appstore op2) {
		//	if (Object.Equals(op1, null)) return Object.Equals(op2, null);
		//	return op1.Equals(op2);
		//}

		//public static bool operator !=(Appstore op1,Appstore op2) {
		//	return !(op1 == op2);
		//}

		//public override bool Equals(object obj) {
		//	Appstore appstore = obj as Appstore;
		//	if (obj == null) return false;
		//	return sAppstore == appstore.sAppstore;
		//}

		//public override int GetHashCode() {
		//	return sAppstore.GetHashCode ();
		//}
	}
	*/
}
