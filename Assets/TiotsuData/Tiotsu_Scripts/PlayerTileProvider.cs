using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Map;

public class PlayerTileProvider : AbstractTileProvider {

	[SerializeField]
	private Transform locationMarker;

	[SerializeField]
	private AbstractMap map;

	[SerializeField]
	private int _visibleBuffer;

	[SerializeField]
	private int _disposeBuffer;

	private bool _initialized = true;
	private Vector2d _currentLatLng;
	private UnwrappedTileId _currentTile;
	private UnwrappedTileId _cachedTile;

	void Start() {
		Initialize (map);
	}

	internal override void OnInitialized() {
	}

	float _elapsedTime;

	[SerializeField]
	float _updateInterval;

	private void Update() 
	{
		if ( !_initialized ) return;

		_elapsedTime += Time.deltaTime;
		if (_elapsedTime >= _updateInterval)
		{
			_elapsedTime = 0f;
			_currentLatLng = locationMarker.position.GetGeoPosition ( map.CenterMercator, map.WorldRelativeScale );

			_currentTile = TileCover.CoordinateToTileId ( _currentLatLng, map.Zoom );

			if ( !_currentTile.Equals ( _cachedTile ) ) 
			{
				for ( int x = _currentTile.X - _visibleBuffer; x <= (_currentTile.X + _visibleBuffer); x++ ) 
				{
					for ( int y = _currentTile.Y - _visibleBuffer; y <= (_currentTile.Y + _visibleBuffer); y++ ) 
					{
						AddTile ( new UnwrappedTileId ( map.Zoom, x, y ) );
					}
				}
				_cachedTile = _currentTile;
				//Cleanup ( _currentTile );
			}
		}
	}

	/* Clean Up ch gadbad aaaa 
	 private void Cleanup( UnwrappedTileId currentTile ) {
		var count = _activeTiles.Count;
		for ( int i = count - 1; i >= 0; i-- ) {
			Dictionary<UnwrappedTileId,byte> tile = _activeTiles[i];
			bool dispose = false;
			dispose = tile.X > currentTile.X + _disposeBuffer || tile.X < _currentTile.X - _disposeBuffer;
			dispose = dispose || tile.Y > _currentTile.Y + _disposeBuffer || tile.Y < _currentTile.Y - _disposeBuffer;

			if ( dispose ) {
				RemoveTile ( tile );
			}
		}
	}*/
}