import React from 'react';
import LabelCheckbox from '../../../../components/ui/molecules/labelCheckbox';
import { useServerFilter, useUpdateServerFilter } from '../ServerFilter';

interface MapNamesFilterSectionProps {
  mapNames: string[];
}

const MapNamesFilterSection: React.FC<MapNamesFilterSectionProps> = (props) => {
  const { mapNames } = useServerFilter();
  const { setMapFilter } = useUpdateServerFilter();
  return (
    <section>
      <header>Map name</header>
      <div className="pl-2">
        {props.mapNames.sort().map((mapName) => (
          <div key={mapName}>
            <LabelCheckbox
              id={mapName}
              label={mapName}
              onChange={() => {
                const index = mapNames.indexOf(mapName);
                if (index === -1) {
                  setMapFilter([...mapNames, mapName]);
                } else {
                  mapNames.splice(index, 1);
                  setMapFilter([...mapNames]);
                }
              }}
              isChecked={mapNames.indexOf(mapName) !== -1}
            />
          </div>
        ))}
      </div>
    </section>
  );
};

export default MapNamesFilterSection;
