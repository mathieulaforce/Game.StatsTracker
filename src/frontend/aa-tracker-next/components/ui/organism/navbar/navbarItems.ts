export const getNavbarItems = (): { id: string; displayName: string; url: string }[] => {
  console.count('get navbars');
  return [
    {
      id: 'home',
      displayName: 'home',
      url: '/',
    },
    {
      id: 'servers',
      displayName: 'servers',
      url: '/serverBrowser',
    },
  ];
};
