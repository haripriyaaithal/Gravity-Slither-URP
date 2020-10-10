using UnityEngine;

namespace GS.Utilities {
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshCollider))]
	public class MeshCombiner : MonoBehaviour {
		private void Start() {
			CombineMeshes();
		}
		
		private void CombineMeshes() {
			var v_meshFilters = GetComponentsInChildren<MeshFilter>();
			var v_combines = new CombineInstance[v_meshFilters.Length];

			for (var v_index = 0; v_index < v_meshFilters.Length; v_index++) {
				v_combines[v_index].mesh = v_meshFilters[v_index].sharedMesh;
				v_combines[v_index].transform = v_meshFilters[v_index].transform.localToWorldMatrix;
				v_meshFilters[v_index].gameObject.SetActive(false);
			}
			
			var v_meshFilter = transform.GetComponent<MeshFilter>();
			v_meshFilter.mesh = new Mesh();
			v_meshFilter.mesh.CombineMeshes(v_combines);
			GetComponent<MeshCollider>().sharedMesh = v_meshFilter.mesh;

			var v_myTransform = transform;
			v_myTransform.gameObject.SetActive(true);
			v_myTransform.localScale = Vector3.one;
			v_myTransform.rotation = Quaternion.identity;
			v_myTransform.position = Vector3.zero;
		}
	}
}