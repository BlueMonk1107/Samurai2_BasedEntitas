using UnityEngine;
using System.Collections;

/**
 *	需要追加在其他脚本之后，否则无法显示在最前面
 *	
 */
public class BasicDialog : MonoBehaviour {
	
	public GUISkin guiSkin;
	public float x = .125f;
	public float y = .125f;
	public float width = .75f;
	public float height = .75f;
	public string title = "标题";
	public string[] texts;
	public Font titleFont;
	public Font textFont;
	public Texture dialogBG;
	public Texture titleBG;
	public State _state = State.Show;
	
	public static State state = State.Hide;
	
	public enum State {
		Show,
		Hide
	}
	
	private int index;
	private float padding = 0.02f;
	private float leftX;
	private float midX;
	private float rightX;
	private float btnY;
	private float btnWidth = 0.167f;
	private float btnHeight = 0.10f;
	
	// title;
	private float titleX;
	private float titleY;
	private float titleWidth;
	private float titleHeight;
	private float titlePadding;
	
	// message
	private float bodyX;
	private float bodyY;
	private float bodyWidth;
	private float bodyHeight;
	
	private void reset(){
		index = 0;
		
		float screenSize = Screen.width;
		if(screenSize > Screen.height){
			screenSize = Screen.height;
		}
		
		btnHeight = (32f * screenSize / 320f) / Screen.height;
		btnWidth = width / 2 - padding * 2;
		leftX = x + padding;
		midX = x + (width - btnWidth) / 2;
		rightX = x + width / 2 + padding;
		btnY = y + height - padding - btnHeight;
		
		
		
		// title
		titleX = x + .01f;
		titleY = y + .01f;
		titleWidth = width - .02f;
		titleHeight = btnHeight;
		titlePadding = (4 * screenSize / 320f) / Screen.height;
		
		
		// message
		bodyX = x + padding;
		bodyY = titleY + titleHeight + padding;
		bodyWidth = width - padding * 2;
		bodyHeight = height - (bodyY - y) - btnHeight - padding * 3;
	}
	
	public void show(){
		reset();
		state = State.Show;
	}
	
	public void hide(){
		state = State.Hide;
		reset();
	}
	
	void Awake() {
		state = _state;
		if(texts != null){
			for(int i = 0; i < texts.Length; i++){
				if(The9Settings.appstore == The9Settings.Appstore.mm){
					texts[i] = texts[i].Replace("游戏名：", "游戏名：MM-");
					texts[i] = texts[i].Replace("版本：", "MM版本：");
				}
				if(The9Settings.appstore == The9Settings.Appstore.wostore ||
					The9Settings.appstore == The9Settings.Appstore.estore){
					//texts[i] = texts[i].Replace("版本：2.0.0", "版本：4.0.0");
				}
			}
		}
		reset();
	}

	void Start () {
		
	}
	
	void Update () {
		
	}
	
	void OnGUI() {
		GUISkin tempSkin = GUI.skin;
		if(guiSkin != null){
			GUI.skin = guiSkin;
		}
		
		if(state == State.Show) {
			
			//GUI.DrawTexture(Rect(Screen.width * x, Screen.height * y, Screen.width * width, Screen.height * height), dialogBG, ScaleMode.ScaleToFit, false, 0);
			//GUI.DrawTexture(Rect(Screen.width * 0.18, Screen.height * 0.28, Screen.width * 0.64, Screen.height * 0.44), dialogBg);
			if(dialogBG != null){
				GUI.DrawTexture(new Rect(Screen.width * x, Screen.height * y, Screen.width * width, Screen.height * height), dialogBG);
			}
			
			if(titleBG != null){
				GUI.DrawTexture(new Rect(Screen.width * titleX, Screen.height * titleY, Screen.width * titleWidth, Screen.height * titleHeight), titleBG);
			}
			
			if(title != null && title != ""){
				if(titleFont != null){
					GUI.skin.font = titleFont;
				}
				GUI.Label(new Rect(Screen.width * (titleX + padding), Screen.height * (titleY + titlePadding), Screen.width * (titleWidth - padding * 2), Screen.height * (titleHeight - titlePadding * 2)), title);
			}
			
			if(texts != null && texts.Length != 0){
				if(textFont != null){
					GUI.skin.font = textFont;
				}
				GUI.Label(new Rect(Screen.width * bodyX, Screen.height * bodyY, Screen.width * bodyWidth, Screen.height * bodyHeight), texts[index]);
				if(index == texts.Length - 1){
					if(titleFont != null){
						GUI.skin.font = titleFont;
					}
					if(GUI.Button(new Rect(Screen.width * midX, Screen.height * btnY, Screen.width * btnWidth, Screen.height * btnHeight), "关闭")){
						hide();
					}
				}else { 
					if(index < texts.Length - 1){
						if(titleFont != null){
							GUI.skin.font = titleFont;
						}
						if(GUI.Button(new Rect(Screen.width * leftX, Screen.height * btnY, Screen.width * btnWidth, Screen.height * btnHeight), "关闭")){
							hide();
						}
						if(GUI.Button(new Rect(Screen.width * rightX, Screen.height * btnY, Screen.width * btnWidth, Screen.height * btnHeight), "下一页")){
							index++;
						}
					}
				}
			}else {
				if(titleFont != null){
					GUI.skin.font = titleFont;
				}
				if(GUI.Button(new Rect(Screen.width * midX, Screen.height * btnY, Screen.width * btnWidth, Screen.height * btnHeight), "关闭")){
					hide();
				}
			}
		}
		GUI.skin = tempSkin;
	}
}