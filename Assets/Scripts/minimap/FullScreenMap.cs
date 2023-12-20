using System;
using ScriptableObjects;
using UnityEngine;

namespace minimap
{
    public class FullScreenMap : MonoBehaviour
    {
        private static FullScreenMap _instance;

        public static FullScreenMap Instance => _instance;


        [SerializeField] private GameObject map;
        [SerializeField] private GameObject closeButton;

        [SerializeField] private TripPlanner tripPlanner;
        
        
        [SerializeField] private MapSettingsSO mainMapSetting;
        [SerializeField] private RectTransform mainCanvas;
        [SerializeField] private RectTransform mainParent;


        [SerializeField] private MapSettingsSO miniMapSettings;
        [SerializeField] private RectTransform miniMapCanvas;

        [SerializeField] private TileManagerUI tileManagerUI;
        
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            map.SetActive(true);
            HideMap();
        }


        public Interactable3dPoi InteractingInteractable3dPoi
        {
            set
            {
                tripPlanner.InteractingInteractable3dPoi = value;
                ShowMap();
            }
        }

        /**
         * Show the full screen map and the close button
         */
        public void ShowMap()
        {
            closeButton.SetActive(true);
            MoveToMainMap();
            tileManagerUI.UpdateMap();

        }

        /**
         * Hide the full screen map and the close button
         */
        public void HideMap()
        {
           // map.SetActive(false);
            closeButton.SetActive(false);
            tripPlanner.ClearCurrentData();
            MoveToMinimap();
            tileManagerUI.UpdateMap();
        }
        
        private void MoveToMinimap()
        {
            transform.SetParent(miniMapCanvas);
            transform.localPosition = Vector3.zero;
            //transform.localScale = miniMapSettings.MapScale;
            //transform.localRotation = Quaternion.Euler(0, 0, 0);

            tileManagerUI.canvasRectTransform = miniMapCanvas;
        }
        
        public void MoveToMainMap()
        {
            transform.SetParent(mainParent);
            transform.localPosition = Vector3.zero;
            //transform.localScale = mainMapSetting.MapScale;
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
            
            tileManagerUI.canvasRectTransform = mainCanvas;
        }

    


        public void ClickedPoi(MiniMapPOI poi)
        {
            switch (poi.PoiType)
            {
                case PoiType.BussStation:
                    tripPlanner.HandleBussStationClick(poi);
                    break;
            }
        }
    }
}