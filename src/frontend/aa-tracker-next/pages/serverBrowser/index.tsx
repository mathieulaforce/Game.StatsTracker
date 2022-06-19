import type { NextPage } from 'next';
import { ServerFilterProvider } from '../../features/serverBrowser/filter/ServerFilter';
import ServerBrowserOverview from '../../features/serverBrowser/serverBrowser.overview';

const ServerBrowserHomePage: NextPage = () => {
  return (
    <ServerFilterProvider>
      <div className="flex gap-2 flex-col">
        <h1>Server browser AA 2.5</h1>
        <ServerBrowserOverview />
      </div>
    </ServerFilterProvider>
  );
};

export default ServerBrowserHomePage;
