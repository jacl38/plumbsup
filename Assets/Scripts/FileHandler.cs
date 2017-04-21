using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData : System.Object
{
	public SaveData() {}
	private List<Score> scores = new List<Score>();
	public List<Score> getScores() { return scores; }
	public void addScore(int difficulty, int score)
	{
		scores.Add(new Score(difficulty, score));
		SortScores();
		scores.Reverse();
	}

	public void SortScores()
	{
		int n = scores.Count;
		while(n != 0)
		{
			int newn = 0;
			for(int i = 1; i < n; i++)
			{
				if(scores[i-1].getScore() > scores[i].getScore())
				{
					Score temp = scores[i];
					scores[i] = scores[i - 1];
					scores[i - 1] = temp;
					newn = i;
				}
			}
			n = newn;
		}
		List<Score> result = new List<Score>();
		for(int i = DifficultySelect.MaxDifficulty; i >= DifficultySelect.MinDifficulty; i--)
		{
			List<Score> sorted = new List<Score>();
			foreach(Score s in scores)
			{
				if(s.getDifficulty() == i)
					sorted.Add(s);
			}
			n = sorted.Count;
			while(n != 0)
			{
				int newn = 0;
				for(int j = 1; j < n; j++)
				{
					if(sorted[j-1].getScore() > sorted[j].getScore())
					{
						Score temp = sorted[j];
						sorted[j] = sorted[j - 1];
						sorted[j - 1] = temp;
						newn = j;
					}
				}
				n = newn;
			}
			sorted.Reverse();
			foreach(Score s in sorted)
			{
				result.Add(s);
			}
		}
		scores = result;
	}

	public void clear()
	{
		scores = new List<Score>();
	}
}

[System.Serializable]
public class Score
{
	int difficulty, score;
	public Score(int difficulty, int score)
	{
		this.difficulty = difficulty;
		this.score = score;
	}

	public int getDifficulty() { return difficulty; }
	public int getScore() { return score; }
}

public class FileHandler : MonoBehaviour {

	void Start () {}
	
	void Update () {}

	public static void Save(SaveData data, string filename)
	{
		string path = pathForDocumentsFile(filename);
		FileStream file = new FileStream(path + ".plumb", FileMode.Create, FileAccess.Write);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, data);
		file.Close();
	}

	public static void Load(ref SaveData loadVariable, string filename)
	{
		if(File.Exists(pathForDocumentsFile(filename) + ".plumb"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(pathForDocumentsFile(filename) + ".plumb", FileMode.Open);
			loadVariable = (SaveData) bf.Deserialize(file);
			file.Close();
		}
	}

	public static string pathForDocumentsFile(string filename)
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			string path = Application.persistentDataPath;
			path = path.Substring(0, path.LastIndexOf('/'));
			return path + "Documents" + filename;
		}
		else
		{
			string path = Application.dataPath;
			path = path.Substring(0, path.LastIndexOf('/'));
			return path + "/" + filename;
		}
	}
}
