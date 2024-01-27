using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scorehandler : MonoBehaviour
{
    public Sprite[] sprites;
    public Image[] images;
    private Vector3[] img_pos_smooth;
    private Vector3 ref_vel = new Vector3(0,0,0);
    public Text score_text;
    private Vector3 score_center_pos;
    public class score_obj
    {
        public bool first_updt_for_text = true;
        public int sprite_number;
        public int score;
        public float display_time = 2f;
        public float wait_time=0.5f;//specific
        public float size=2;
        public string str;
    }
    private float global_wait_timer=0.3f;
    public score_obj[] display_quee=new score_obj[10];
    private int display_tail = -1;
    public score_obj[] waiting_quee= new score_obj[10];
    private int waiting_tail = -1;
    
   
    // Start is called before the first frame update
    private void Awake()
    {
        display_tail = -1;
        waiting_tail =-1;
        score_center_pos = new Vector3(0,0,0);
        for(int i = 0;i<images.Length;i++)
        {
            images[i].enabled = false;
        }
        score_text.text = " ";
        img_pos_smooth = new Vector3[10];
    }
    public void call_once_for_score(int sp_num,int score,string rcvd_str)
    {
        input_wait_quee(sp_num, score, rcvd_str);

    }
    private void input_wait_quee(int sp_num, int score,string rcvd_str)
    {
        waiting_tail++;
        waiting_quee[waiting_tail]=new score_obj();
        waiting_quee[waiting_tail].sprite_number=sp_num;
        waiting_quee[waiting_tail].score=score;
        waiting_quee[waiting_tail].str=rcvd_str+ " +"+ score.ToString()+ "Xp";
       
    }
    private void wait_quee_updt()
    {
        for(int i=0;i<=waiting_tail;i++)
        {
            if(waiting_quee[i]!=null)
            {
                if(/*waiting_quee[i].wait_time*/global_wait_timer<=0)
                {
                    global_wait_timer = 0.3f;
                    //insert to display
                    display_tail++;
                    display_quee[display_tail] = new score_obj();
                    display_quee[display_tail]=waiting_quee[i];
                    images[display_tail].enabled=true;
                    images[display_tail].sprite = sprites[display_quee[display_tail].sprite_number];
                    //smooth_pos
                    img_pos_smooth[display_tail] = images[display_tail].rectTransform.localPosition;
                    //sizing
                    images[display_tail].rectTransform.localScale = new Vector3(display_quee[display_tail].size, display_quee[display_tail].size, display_quee[display_tail].size);
                    //end of sizing
                    waiting_quee[i] = null;
                    //shift wait wuee elements
                    for (int j=i;j<=waiting_tail;j++)
                    {
                        waiting_quee[j]=waiting_quee[j+1];
                        waiting_quee[j+1] = null;
                    }
                    //decrease tail
                    waiting_tail--;
                }
                else
                {
                    //waiting_quee[i].wait_time-=Time.deltaTime;
                    global_wait_timer -= Time.deltaTime;
                }
            }
        }
    }
    private void display_quee_updt()
    {
        for (int i = 0; i <= display_tail; i++)
        {
            if (display_quee[i] != null)
            {
                if (display_quee[i].display_time <= 0)
                {
                    //score_text_prev_textt
                    score_text.text = score_text.text.Remove(score_text.text.Length - (display_quee[i].str.Length + 1));
                    score_text.rectTransform.localPosition += new Vector3(0, 250*score_text.rectTransform.localScale.y, 0);
                    //pop out
                    display_quee[i] = null;
                    for(int j=i;j<=display_tail;j++)
                    {
                        display_quee[j] = display_quee[j+1];
                        images[j].sprite = images[j+1].sprite;
                        images[j + 1].sprite = null;
                        display_quee[j+1]=null;
                    }
                    images[display_tail].enabled = false;
                    //setting the  smooth pos to 00;
                    img_pos_smooth[display_tail]=new Vector3(0,0,0);    
                    display_tail--;
                }
                else
                {
                    display_quee[i].display_time -= Time.deltaTime;
                    //size decrease
                    display_quee[i].size -= (10f-300*Time.fixedDeltaTime) * Time.fixedDeltaTime; 
                    if (display_quee[i].size <= 0.4f)//
                        display_quee[i].size = 0.4f;
                    images[i].rectTransform.localScale = new Vector3(display_quee[i].size, display_quee[i].size, display_quee[i].size);
                    // text
                    if(display_quee[i].first_updt_for_text)
                    {
                        display_quee[i].first_updt_for_text = false;
                        score_text.text += display_quee[i].str + "\n";
                        //shifting donwnward
                        score_text.rectTransform.localPosition -= new Vector3(0, 250*score_text.rectTransform.localScale.y, 0);
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    wait_quee_updt();
    display_quee_updt();
        //set_images_position based on number of kills
        if((display_tail+1)%2==0)
        {
            //smooth
            img_pos_smooth[0]=score_center_pos+ new Vector3((Mathf.FloorToInt((display_tail + 1) / 2) * (images[0].rectTransform.sizeDelta.x)) - (images[0].rectTransform.sizeDelta.x / 2), 0, 0);
            images[0].rectTransform.localPosition = Vector3.SmoothDamp(images[0].rectTransform.localPosition, img_pos_smooth[0], ref ref_vel, 0.2f);
           // images[0].rectTransform.localPosition = score_center_pos + new Vector3((Mathf.FloorToInt((display_tail + 1) / 2) * (images[0].rectTransform.sizeDelta.x))- (images[0].rectTransform.sizeDelta.x/2), 0, 0);//50 is offset between two images
        }
        else
        {
            images[0].rectTransform.localPosition = score_center_pos + new Vector3(Mathf.FloorToInt((display_tail + 1) / 2) * (images[0].rectTransform.sizeDelta.x), 0, 0);//50 is offset between two images
           
        }
        for (int i = 1; i <= images.Length; i++)
        {
            images[i].rectTransform.localPosition = images[0].rectTransform.localPosition - new Vector3(i * (images[i].rectTransform.sizeDelta.x), 0, 0);
        }
        
    }
}
