using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class leave_room : MonoBehaviour
{
   public void leave()
    {
        PhotonNetwork.LeaveRoom(false);
    }
}
