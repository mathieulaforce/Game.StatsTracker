import type { NextPage } from 'next';
import Head from 'next/head';
import Image from 'next/image';
import { spin } from '@heroicons/react/solid';
const Home: NextPage = () => {
  return (
    <div>
      <Head>
        <title>Create Next App</title>
        <meta name="description" content="Generated by create next app" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <h1 className="text-3xl font-bold underline text-right">Hello world!</h1>
      <main>welcome</main>
      
      <footer></footer>
    </div>
  );
};

export default Home;
