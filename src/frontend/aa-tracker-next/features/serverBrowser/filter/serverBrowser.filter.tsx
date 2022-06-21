import React from 'react';
import MapNamesFilterSection from './sections/mapNameFilterSection';
import PlayersFilterSection from './sections/playersFilterSection';
import ServerStatusFilterSection from './sections/serverStatusFilterSection';
import { useUpdateServerFilter } from './ServerFilter';
import { XIcon } from '@heroicons/react/solid';
import ToolTip from '../../../components/ui/atoms/toolTip/toolTip';

interface ServerBrowserFilterProps {
  mapNames: string[];
}

const ServerBrowserFilter: React.FC<ServerBrowserFilterProps> = (props) => {
  const { resetFilter } = useUpdateServerFilter();
  return (
    <section className="p-2 bg-slate-800 rounded-xl shadow-lg   ring-1 ring-white/10 ring-inset text-white/80">
      <div className="flex gap-4 items-center">
        <ServerStatusFilterSection />
        <PlayersFilterSection />
        <MapNamesFilterSection mapNames={props.mapNames} />
        <ToolTip message="Reset filter">
          <span className="cursor-pointer text-red-500 w-5 " onClick={() => resetFilter()}>
            <XIcon className=" w-5 hover:rotate-12 transition-all" />
          </span>
        </ToolTip>
      </div>
    </section>
  );
};

export default ServerBrowserFilter;
