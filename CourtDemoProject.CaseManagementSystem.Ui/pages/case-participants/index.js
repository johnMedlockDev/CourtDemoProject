import PropTypes from 'prop-types'
import axios from 'axios'
import Link from 'next/link'

const CaseParticipantsPage = ({ caseParticipants }) => {
	return (
		<div>
			<h1>Case Participants</h1>
			<ul>
				{caseParticipants.map((participant) => (
					<li key={participant.caseParticipantEntityId}>
						<Link href={`/case-participants/${participant.caseParticipantEntityId}`}>
							<a>
								<p>Name: {participant.caseParticipantFirstName} {participant.caseParticipantMiddleName} {participant.caseParticipantLastName}</p>
								<p>Type: {participant.caseParticipantType}</p>
							</a>
						</Link>
					</li>
				))}
			</ul>
		</div>
	)
}

export const getServerSideProps = async () => {
	const res = await axios.get('http://api:8080/v1/CaseParticipants')
	const caseParticipants = res.data // Adjust this according to the API response

	return {
		props: { caseParticipants }
	}
}

CaseParticipantsPage.propTypes = {
	caseParticipants: PropTypes.arrayOf(
		PropTypes.shape({
			caseParticipantEntityId: PropTypes.string.isRequired,
			caseParticipantType: PropTypes.number.isRequired,
			caseParticipantFirstName: PropTypes.string.isRequired,
			caseParticipantLastName: PropTypes.string.isRequired,
			caseParticipantMiddleName: PropTypes.string
		})
	).isRequired
}

export default CaseParticipantsPage
