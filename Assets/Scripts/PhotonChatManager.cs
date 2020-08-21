using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{

    public ChatClient chatClient;
    public InputField plrName;
    public Text connectionState;
    string worldChat;
    public InputField msgInput;
    public Text msgArena;

    public GameObject introPanel;
    public GameObject messagePanel;

    //[SerializeField] string userID;

    public void DebugReturn(DebugLevel level, string message)
    {
        ;
    }

    public void OnChatStateChange(ChatState state)
    {
        ;
    }

    public void OnConnected()
    {
        introPanel.SetActive(false);
        messagePanel.SetActive(true);
        connectionState.text = "Connected";
        this.chatClient.Subscribe(new string[] { worldChat });
        this.chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        ;
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            msgArena.text += senders[i] + " : " + messages[i] + "\n";
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        ;
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        ;
    }

    public void OnUnsubscribed(string[] channels)
    {
        ;
    }

    public void OnUserSubscribed(string channel, string user)
    {
        ;
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        ;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat))
        {
            print("No chat ID provided.");
            return;
        }

        connectionState.text = "Connecting...";
        worldChat = "World";
    }

    public void getConnected()
    {
        print("Trying to connect");
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(plrName.text));
        connectionState.text = "Connecting to server";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.chatClient != null)
            chatClient.Service();
    }

    public void sendMsg()
    {
        this.chatClient.PublishMessage(worldChat, msgInput.text);
    }
}
