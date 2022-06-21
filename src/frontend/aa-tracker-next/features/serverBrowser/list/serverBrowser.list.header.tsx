import React from 'react';

interface ServerBrowserListHeaderProps {}

const ServerBrowserListHeader: React.FC<ServerBrowserListHeaderProps> = () => {
  return (
    <thead className="bg-slate-800 text-white rounded-xl h-11 ring-1 ring-white/10 ring-inset">
      <tr>
        <th></th>
        <th className="text-left p-2 capitalize">
          <span>Name</span>
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
