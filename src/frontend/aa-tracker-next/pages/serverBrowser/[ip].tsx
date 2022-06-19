import type { NextPage } from 'next';
import { useRouter } from 'next/router';
import { ServerFilterProvider } from '../../features/serverBrowser/filter/ServerFilter';
import ServerDetails from '../../features/serverBrowser/server/serverDetails';
import ServerBrowserOverview from '../../features/serverBrowser/serverBrowser.overview';

const ServerDetailPage: NextPage = () => {
  const router = useRouter();
  const { ip } = router.query;

  return <ServerDetails ip={ip?.toString() ?? ''}></ServerDetails>;
};

export default ServerDetailPage;
