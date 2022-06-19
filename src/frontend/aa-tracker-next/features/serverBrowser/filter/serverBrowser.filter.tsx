import React from 'react';
import MapNamesFilterSection from './sections/mapNameFilterSection';
import PlayersFilterSection from './sections/playersFilterSection';
import ServerStatusFilterSection from './sections/serverStatusFilterSection';
import { useUpdateServerFilter } from './ServerFilter';

interface ServerBrowserFilterProps {
  mapNames: string[];
}

const ServerBrowserFilter: React.FC<ServerBrowserFilterProps> = (props) => {
  const { resetFilter } = useUpdateServerFilter();
  return (
    <section className="border border-slate-500 rounded-md">
      <header className="bg-slate-500 text-white p-2 flex justify-between">
        <h3>Filter</h3>
        <button className="p-1 px-2 rounded-xl text-sm right text-white m-0 bg-slate-900 hover:bg-slate-100 hover:text-slate-900 transition" onClick={resetFilter}>
          reset
        </button>
      </header>
      <div className="p-2">
        <ServerStatusFilterSection />
        <PlayersFilterSection />
        <MapNamesFilterSection mapNames={props.mapNames} />
      </div>
    </section>
  );
};

export default ServerBrowserFilter;
