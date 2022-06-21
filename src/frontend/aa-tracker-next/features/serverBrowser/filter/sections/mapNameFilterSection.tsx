import React, { useState } from 'react';
import Button from '../../../../components/ui/atoms/buttons/button';
import LabelCheckbox from '../../../../components/ui/molecules/labelCheckbox';
import { useServerFilter, useUpdateServerFilter } from '../ServerFilter';
import { PlusIcon, XIcon } from '@heroicons/react/solid';

interface MapNamesFilterSectionProps {
  mapNames: string[];
}

const MapNamesFilterSection: React.FC<MapNamesFilterSectionProps> = (props) => {
  const { mapNames } = useServerFilter();
  const { setMapFilter } = useUpdateServerFilter();

  return (
    <section className="flex items-center">
      <div className="group relative inline-block mx-1 mb-1 ">
        <Button variant="primary" className="px-2 text-sm peer" disabled={mapNames.length === 5}>
          <PlusIcon className="w-3" />
          Map
        </Button>

        <div id="dropdownBottom" className="z-10 rounded shadow mt-2 bg-slate-700 hidden top-5 group-hover:block absolute peer-disabled:hidden ">
          <div className="w-max">
            <div className=" grid grid-cols-3 text-sm w-fit text-gray-200" aria-labelledby="dropdownBottomButton">
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
        </div>
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
