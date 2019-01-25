using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCanvasController : MonoBehaviour {

    public List<CanvasGroup> cvs;
    public List<MeshRenderer> cubes;
    public float animation_time = 1f;

    private int currSlide;

	// Use this for initialization
	void Start () {
	    if(cvs.Count < 6)
        {
            Debug.Log("The canvas group count is wrong");
        }

        
        for(int i = 0; i < 6; i++)
        {
            cvs[i].gameObject.SetActive(true);
            cvs[i].alpha = 0f;
        }

        currSlide = 0;
        cvs[0].alpha = 1;
        cubes[0].material.color = Color.cyan;
	}

    public void OnEnable()
    {
        StartCoroutine(SlideAnimation());
    }

    private IEnumerator SlideAnimation()
    {

        while (true)
        {
            int nextSlide = GetNextSlide(currSlide);

            yield return new WaitForSeconds(animation_time);

            yield return SwitchSlide(currSlide, nextSlide);

            currSlide = nextSlide;

            yield return null;
        }
        
    }

    private IEnumerator SwitchSlide(int curr, int next)
    {
        CanvasGroup curr_slide = cvs[curr];
        CanvasGroup next_slide = cvs[next];
        MeshRenderer curr_cube = cubes[curr];
        MeshRenderer next_cube = cubes[next];
        curr_cube.material.color = Color.white;
        next_cube.material.color = Color.cyan;

        while (next_slide.alpha < 1)
        {
            curr_slide.alpha -= 0.1f;
            next_slide.alpha += 0.1f;
            yield return null;
        }

        curr_slide.alpha = 0f;
        next_slide.alpha = 1f;
        

        yield return null;

    }

    private int GetNextSlide(int curr)
    {
        if(curr >= 6 && curr < 0)
        {
            Debug.Log("curr is wrong here");
            return -1;
        }

        if(curr == 5)
        {
            return 0;
        }
        else
        {
            return (curr + 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
