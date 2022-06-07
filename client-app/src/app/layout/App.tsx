import React, { useEffect, useState } from 'react';
import UserForm from './../../features/user-form/user-form';
import { User } from './../models/user';
import {v4 as uuid} from 'uuid';
import agent from './../api/agent';
import { SectorOption } from '../models/sector-option';

function App() {

  const newUser: User = {
    id: '',
    name: '',
    isAgreed: false,
    sectorOptionIds: Array<string>()
  };

  const[user, setUser] = useState(newUser);
  const[sectorOptions, setSectorOptions] = useState<SectorOption[]>([]);
  const[submitting, setSubmitting] = useState(false);

  useEffect(() => {
    agent.sectorOptions.list().then(response => {
      setSectorOptions(response)
    })
  }, [])

  function handleUpdateUser(user: User) {
    setSubmitting(true);
    if (!user.id) {
      user.id = uuid();
    }

    agent.users.edit(user).then(() => {
      setSubmitting(false);
    })

    setUser(user);
  } 

  return (
    <div>
      <h1>
        Please enter your name and pick the Sectors you are currently involved
        in.
      </h1>
      <UserForm
        sectorOptions={sectorOptions}
        user={user}
        update={handleUpdateUser}
        submitting={submitting}/>
    </div>
  );
}

export default App;
