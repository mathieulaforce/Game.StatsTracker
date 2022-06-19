import React from 'react';

interface ServerBrowserListHeaderProps {}

const ServerBrowserListHeader: React.FC<ServerBrowserListHeaderProps> = () => {
  return (
    <thead className="bg-slate-500 text-white rounded-xl h-11">
      <tr>
        <th></th>
        <th className="text-left p-2 capitalize">
          <span>Name</span>
          {/* <div className="relative text-black inline ml-2">
            <span className="absolute inset-y-0 left-0 flex items-center pl-2">
              <button type="submit" className="p-1 focus:outline-none focus:shadow-outline">
                <SearchIcon className="w-6 h-6" />
              </button>
            </span>
            <input
              type="search"
              autoComplete="off"
              className="py-2 text-sm text-black bg-slate-300 rounded-md pl-10 focus:outline-none focus:bg-slate-200 focus:text-gray-900"
              placeholder="Name..."
              value={filter.name}
              onChange={(e) => onFilterChanged(e.target.value, filter.mapName, 'all')}
            />
          </div> */}
        </th>
        <th className="text-left p-2 capitalize">
          <span>ip</span>
        </th>
        <th className="text-left p-2 capitalize">
          <span>map</span>
        </th>
        <th className="text-left p-2 capitalize">players</th>
        <th className="text-left p-2 capitalize">version</th>
      </tr>
    </thead>
  );
};
export default ServerBrowserListHeader;
