using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA; // List of questions and answers
    public GameObject[] options; // Array of answer options
    public int currentQuestion;

    public Text QuestionTxt; // UI Text for displaying the question
    public GameObject QuizPanel; // Panel for the quiz
    public GameObject GoPanel; // Panel for game over

    public Text ScoreTxt; // UI Text for displaying the score
    private int totalQuestions = 0; // Total number of questions
    public int score; // Player score

    private void Start()
    {
        totalQuestions = QnA.Count; // Set total questions from the list
        GoPanel.SetActive(false); // Hide the game over panel initially
        GenerateQuestion(); // Generate the first question
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the quiz
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void Correct()
    {
        score++; // Increment score for correct answer
        QnA.RemoveAt(currentQuestion); // Remove the current question
        GenerateQuestion(); // Generate the next question
    }

    public void Wrong()
    {
        QnA.RemoveAt(currentQuestion); // Remove the current question
        GenerateQuestion(); // Generate the next question
    }

    public void GameOver()
    {
        QuizPanel.SetActive(false); // Hide the quiz panel
        GoPanel.SetActive(true); // Show the game over panel
        ScoreTxt.text = score + "/" + totalQuestions; // Display the score
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i < QnA[currentQuestion].Answers.Count) // Ensure the index is within range
            {
                options[i].GetComponent<AnswerScript>().isCorrect = false; // Reset the isCorrect flag
                options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i]; // Set answer text

                // Check if this option is the correct answer
                if (QnA[currentQuestion].CorrectAnswer == i) // Change to 0-indexing
                {
                    options[i].GetComponent<AnswerScript>().isCorrect = true;
                }
            }
        }
    }

    void GenerateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count); // Select a random question
            QuestionTxt.text = QnA[currentQuestion].Question; // Set the question text
            SetAnswers(); // Set the answer options
        }
        else
        {
            Debug.Log("Out of Questions"); // Log when there are no more questions
            GameOver(); // Trigger game over
        }
    }
}
