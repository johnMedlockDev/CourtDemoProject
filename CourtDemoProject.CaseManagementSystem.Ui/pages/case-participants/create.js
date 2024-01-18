import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import styles from '../../styles/pages/case-participants/CreateParticipant.module.scss'

const CreateCaseParticipantPage = () => {
	const [participantData, setParticipantData] = useState({
		caseParticipantFirstName: '',
		caseParticipantLastName: '',
		caseParticipantMiddleName: '',
		caseParticipantType: '' // Assuming this is an enum or similar
		// Add other fields as needed
	})
	const router = useRouter()

	const handleChange = (e) => {
		setParticipantData({ ...participantData, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.post('http://api:8080/v1/CaseParticipants', participantData)
			router.push('/case-participants')
		} catch (error) {
			console.error('Error creating case participant:', error)
		}
	}

	return (
		<div className={styles.createParticipant}>
			<h1>Create New Case Participant</h1>
			<form onSubmit={handleSubmit}>
				<div>
					<label htmlFor="firstName">First Name:</label>
					<input
						type="text"
						id="firstName"
						name="caseParticipantFirstName"
						value={participantData.caseParticipantFirstName}
						onChange={handleChange}
						required
					/>
				</div>
				<div>
					<label htmlFor="lastName">Last Name:</label>
					<input
						type="text"
						id="lastName"
						name="caseParticipantLastName"
						value={participantData.caseParticipantLastName}
						onChange={handleChange}
						required
					/>
				</div>
				<div>
					<label htmlFor="middleName">Middle Name:</label>
					<input
						type="text"
						id="middleName"
						name="caseParticipantMiddleName"
						value={participantData.caseParticipantMiddleName}
						onChange={handleChange}
					/>
				</div>
				<div>
					<label htmlFor="participantType">Participant Type:</label>
					<select
						id="participantType"
						name="caseParticipantType"
						value={participantData.caseParticipantType}
						onChange={handleChange}
						required
					>
						{/* Populate the options dynamically based on your participant types */}
						<option value="">Select Type</option>
						<option value="type1">Type 1</option>
						<option value="type2">Type 2</option>
						{/* ... other participant types ... */}
					</select>
				</div>
				{/* Add other input fields as needed */}
				<button type="submit">Create Participant</button>
			</form>
		</div>
	)
}

export default CreateCaseParticipantPage
