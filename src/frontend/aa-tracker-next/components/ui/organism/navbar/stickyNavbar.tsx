import React from 'react';
import { useMemo, useState } from 'react';
import Link from 'next/link';
import { getNavbarItems } from './navbarItems';
import NavLink from '../../atoms/navigationLink';

const StickyNavbar: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const navigationItems = useMemo(() => getNavbarItems(), []);
  return (
    <div className={`z-40 w-full backdrop-blur bg-transparent border-b border-slate-50/[0.06] `}>
      <div className="mx-auto">
        <div className="py-4 border-b px-8 border-0 border-slate-300/10 mx-0">
          <div className="relative flex items-center">
            <a className="mr-3 flex-none overflow-hidden w-auto" href="#">
              <Link href="/">
                <h1 className="text-slate-200 hover:text-sky-500 duration-200 transition-colors cursor-pointer">Tracker</h1>
              </Link>
            </a>
            <div className="relative flex items-center ml-auto">
              <nav className="font-semibold capitalize text-slate-200">
                <ul className="flex space-x-8">
                  {navigationItems.map((navItem) => {
                    return (
                      <li key={navItem.id} className="hover:text-sky-500 transition-colors duration-200 cursor-pointer">
                        <NavLink onClick={() => setIsOpen(false)} key={navItem.id} href={navItem.url}>
                          <div>{navItem.displayName}</div>
                        </NavLink>
                      </li>
                    );
                  })}
                </ul>
              </nav>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default StickyNavbar;
