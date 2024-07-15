using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 3.0f;
    public bool hasPowerUp = false;
    private float powerUpStrength = 15.0f;
    public int score;
    public int kills;
    public bool isGameOn = false;

    public GameObject powerUpIndicator;

    private GameManager gameManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager instance

        isGameOn = true;
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (transform.position.y < -10 && isGameOn)
        {
            GameOver();
        }
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        gameManager.UpdateScore(score);
    }

    public void AddKill(int addKill)
    {
        kills += addKill;
        gameManager.UpdateKills(kills);
    }

    public void GameOver()
    {
        isGameOn = false;
        Debug.Log("The Score is " + score);


        StartCoroutine(SendScoreToAPI(
            "self_rewards", "473993", "typeJk", "3947", "9387-423",
            "Derek", "SP101", "SphereSurvival", "0", "0", kills, 0,
            score, "image", "1:00 AM", "2:30 AM"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
            powerUpIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidBody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator SendScoreToAPI(
        string operation, string scMemberID, string memberType, string schoolID, string mobileNo,
        string userName, string gameID, string gameName, string exps, string level, int kills,
        int badges, int score, string img, string times, string times2)
    {
        string url = "https://dev.smartcookie.in/core/webservice_game.php";
        WWWForm form = new WWWForm();

        form.AddField("operation", operation);
        form.AddField("SC_Member_ID", scMemberID);
        form.AddField("Member_type", memberType);
        form.AddField("School_id", schoolID);
        form.AddField("mobile_no", mobileNo);
        form.AddField("user_name", userName);
        form.AddField("game_id", gameID);
        form.AddField("game_name", gameName);
        form.AddField("exps", exps);
        form.AddField("level", level);
        form.AddField("kils", kills);
        form.AddField("badges", badges);
        form.AddField("score", score);
        form.AddField("img", img);
        form.AddField("times", times);
        form.AddField("times2", times2);

        Debug.Log("The Score is: " + score);
        if (score > 0)
        {
            Debug.Log("Estimation Reward: " + score / 2);
        }
        else
        {
            Debug.Log("Estimation Reward: 0");
        }

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Data submitted successfully: " + www.downloadHandler.text);
            }

            
        }
    }
}
