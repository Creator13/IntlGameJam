using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simfluencer.UI {
    public class TabGroup : MonoBehaviour {
        [field: NonSerialized] public event Action<object> TabChanged;

        private List<Tab> tabs;

        private Tab selectedTab;

        public Tab SelectedTab {
            get => selectedTab;
            set {
                if (selectedTab) selectedTab.IsSelected = false;
                selectedTab = value;
                selectedTab.IsSelected = true;
            }
        }

        private void Awake() {
            tabs = GetComponentsInChildren<Tab>().ToList();

            foreach (var tab in tabs) {
                tab.TabClicked += SetSelectedTab;
            }
        }

        private void Start() {
            SelectedTab = tabs[0];
        }

        private void OnDestroy() {
            foreach (var tab in tabs) {
                tab.TabClicked -= SetSelectedTab;
            }
        }

        private void SetSelectedTab(Tab tab) {
            SelectedTab = tab;
            TabChanged?.Invoke(tab.Value);
        }
    }
}
