import * as React from 'react';
import { useMemo, useState } from 'react';
import { MenuIcon, XIcon } from '@heroicons/react/solid';
import Link from 'next/link';
import { getNavbarItems } from './navbarItems';
import NavLink from '../../atoms/NavigationLink';

const Navbar: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const navigationItems = useMemo(() => getNavbarItems(), []);
  return (
    <nav className={`bg-slate-500 px-4 shadow-lg md:flex md:justify-between md:items-center`}>
      <div className="flex justify-between items-center py-4">
        <Link href="/">
          <h1 className="text-white cursor-pointer">Tracker</h1>
        </Link>

        <span className="text-base text-white md:hidden cursor-pointer" onClick={() => setIsOpen(!isOpen)}>
          {isOpen ? <XIcon className="w-5" /> : <MenuIcon className="w-5" />}
        </span>
      </div>
      <div
        className={`text-white text-xl  bg-slate-500 w-full opacity-0 left-0 absolute self-stretch
        md:flex md:gap-1 md:w-auto md:opacity-100 md:z-auto md:static  
        ${isOpen ? 'opacity-100' : ''}
      `}
      >
        {navigationItems.map((navItem) => {
          return (
            <NavLink
              onClick={() => setIsOpen(false)}
              key={navItem.id}
              href={navItem.url}
              className={`relative flex flex-col items-center text-center p-4 cursor-pointer capitalize
            before:w-0  before:duration-500 before:left-[50%]
          hover:before:bg-slate-700 hover:before:rounded-sm hover:before:h-1  hover:before:w-full hover:before:absolute hover:before:left-0 hover:before:bottom-0`}
              activeClassName={`relative flex flex-col items-center text-center p-4 cursor-pointer capitalize  
            before:bg-slate-700 before:rounded-sm before:h-1  before:w-full before:absolute before:left-0 before:bottom-0`}
            >
              <div>{navItem.displayName}</div>
            </NavLink>
          );
        })}
      </div>
    </nav>
  );
};
export default Navbar;
