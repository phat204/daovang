using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score, explosive, time, targetScore, level;
    public GameObject mucTieuPanel;
    public TextMeshProUGUI mucTieuText;
    private bool isEndGame;
    public TextMeshProUGUI scoreText, explosiveText, timeText, targetScoreText, levelText;
    public GameObject[] levels;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        targetScore = 650;
        level = 1;

        ResetGame();

        UpdateUI();
        
    }

    private void ResetGame() 
    {
        score = 0;
        time = 60;
        isEndGame = false;
        Time.timeScale = 1;
        StartCoroutine(CountDownTime());
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == level - 1);
        }
        AudioManager.Instance.PlayBackgroundSound();
    }

    private IEnumerator CountDownTime() {
        while(!isEndGame) {
            yield return new WaitForSeconds(1f);
            if (time > 0) 
            {
                time--;
                if (time < 11)
                {
                    AudioManager.Instance.PlayDongHoSound();
                }
            }
            else
            {
                time = 0;
                EndGame();
                AudioManager.Instance.StopBackgroundSound();
            }
        }
    }
    
    private void EndGame() {
        if(isEndGame) return;
        isEndGame = true;
        Time.timeScale = 0;
        mucTieuPanel.SetActive(true);

        if (score >= targetScore)
        {
            if (level < levels.Length)
            {
                AudioManager.Instance.PlayMucTieuSound();
                mucTieuText.text = $"Bạn đã lên cấp!\n Mục tiêu tiếp theo: {targetScore + 600} điểm";
                StartCoroutine(NextLevel());
            }
            else
            {
                AudioManager.Instance.PlayThangSound();
                mucTieuText.text = "Bạn đã hoàn thành tất cả các cấp độ!";
                StartCoroutine(LoadScene(1));
            }
        }
        else
        {
            AudioManager.Instance.PlayThuaSound();
            mucTieuText.text = "Bạn đã không hoàn thành mục tiêu!\n Trò chơi sẽ khởi động lại.";
            StartCoroutine(LoadScene(0));
        }
    }

    private IEnumerator LoadScene(int sceneIndex) {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator NextLevel() {
        yield return new WaitForSecondsRealtime(3f);
        level++;
        targetScore += 600;
        ResetGame();
        mucTieuPanel.SetActive(false);
    }

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString();
        explosiveText.text = explosive.ToString();
        timeText.text = time.ToString();
        targetScoreText.text = targetScore.ToString();
        levelText.text = level.ToString();
    }
}
