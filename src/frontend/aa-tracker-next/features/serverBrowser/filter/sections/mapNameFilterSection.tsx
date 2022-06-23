import React, { useState } from 'react';
import Button from '../../../../components/ui/atoms/buttons/button';
import { useServerFilter, useUpdateServerFilter } from '../ServerFilter';
import { PlusIcon, XIcon } from '@heroicons/react/solid';
import ClickOutsideElementProvider from '../../../../components/ui/utils/ClickOutsideElementProvider';
import Panel from '../../../../components/ui/atoms/panel';

interface MapNamesFilterSectionProps {
  mapNames: string[];
}

const MapNamesFilterSection: React.FC<MapNamesFilterSectionProps> = (props) => {
  const { mapNames } = useServerFilter();
  const { setMapFilter } = useUpdateServerFilter();

  const [showMapsDropdown, setShowMapsDropdown] = useState<'invisible' | 'visible'>('invisible');

  return (
    <section className="flex items-center">
      <div className="group relative inline-block mx-1 mb-1 ">
        <Button
          variant="primary"
          className="px-2 text-sm peer"
          disabled={mapNames.length === 5}
          onClick={() => setShowMapsDropdown(showMapsDropdown === 'invisible' ? 'visible' : 'invisible')}
        >
          <PlusIcon className="w-3" />
          Map ({mapNames.length}/5)
        </Button>

        <Panel className={`z-10 shadow shadow-sky-400 mt-2 ${showMapsDropdown} top-5 absolute peer-disabled:invisible`}>
          <ClickOutsideElementProvider
            onOutsideClick={function (): void {
              if (showMapsDropdown === 'visible') {
                setShowMapsDropdown('invisible');
              }
            }}
          >
            <div className="w-max relative">
              <div className=" grid grid-cols-3 text-sm w-fit text-gray-200">
                {props.mapNames
                  .filter((mapName) => mapNames.indexOf(mapName) === -1)
                  .map((mapName) => {
                    return (
                      <div
                        key={mapName}
                        onClick={() => {
                          setMapFilter([...mapNames, mapName]);
                        }}
                      >
                        <a href="#" className="block px-4 py-2 hover:bg-gray-600 hover:text-white">
                          {mapName}
                        </a>
                      </div>
                    );
                  })}
              </div>
            </div>
          </ClickOutsideElementProvider>
        </Panel>
      </div>
      {mapNames.map((mapName) => {
        return (
          <Button
            key={mapName}
            variant="light"
            className="mx-1 mb-1 m text-sm peer"
            onClick={() => {
              {
                mapNames.splice(mapNames.indexOf(mapName), 1);
                setMapFilter([...mapNames]);
              }
            }}
          >
            <p className="flex items-center">
              {mapName}
              <span>
                <XIcon className="w-3" />
              </span>
            </p>
          </Button>
        );
      })}
    </section>
  );
};

export default MapNamesFilterSection;
